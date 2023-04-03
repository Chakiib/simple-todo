import { createSlice, createAsyncThunk, PayloadAction } from "@reduxjs/toolkit";

import type { RootState } from "../app/store";
import {
  CreateTodoListModel,
  TodoItemModel,
  TodoListModel,
} from "../models/TodoModels";
import {
  getTodoListsAsync,
  createTodoListAsync,
  deleteTodoListsAsync,
  updateTodoListsAsync,
  createTodoItemAsync,
  deleteTodoItemAsync,
  updateTodoItemAsync,
} from "../services/todoService";
import { ApiError } from "../models/ApiModels";

export type Status = "idle" | "loading" | "failed";
export interface TodosState {
  lists: TodoListModel[];
  selectedList?: TodoListModel;
  listLoadingStatus: Status;
  listSavingStatus: Status;
  itemSavingStatus: Status;
}

const initialState: TodosState = {
  lists: [],
  selectedList: undefined,
  listLoadingStatus: "idle",
  listSavingStatus: "idle",
  itemSavingStatus: "idle",
};

export const fetchTodoLists = createAsyncThunk<
  TodoListModel[],
  undefined,
  {
    rejectValue: ApiError;
  }
>("todos/fetchTodoLists", async (_, thunkApi) => {
  const result = (await getTodoListsAsync()) as any;
  if (result.error) {
    return thunkApi.rejectWithValue(result.error as ApiError);
  }
  return result as TodoListModel[];
});

export const createTodoList = createAsyncThunk<
  TodoListModel,
  CreateTodoListModel,
  {
    rejectValue: ApiError;
  }
>("todos/createTodoList", async (value, thunkApi) => {
  const result = (await createTodoListAsync(value)) as any;
  if (result.error) {
    return thunkApi.rejectWithValue(result.error as ApiError);
  }
  return result as TodoListModel;
});

export const deleteTodoList = createAsyncThunk<
  number,
  number,
  {
    rejectValue: ApiError;
  }
>("todos/deleteTodoList", async (todoListId, thunkApi) => {
  const error = await deleteTodoListsAsync(todoListId);
  if (error) {
    return thunkApi.rejectWithValue(error);
  }

  return todoListId;
});

export const updateTodoList = createAsyncThunk<
  TodoListModel,
  TodoListModel,
  {
    rejectValue: ApiError;
  }
>("todos/updateTodoList", async (todoList, thunkApi) => {
  const error = await updateTodoListsAsync(todoList);
  if (error) {
    return thunkApi.rejectWithValue(error);
  }

  return todoList;
});

export const createTodoItem = createAsyncThunk<
  TodoItemModel,
  { todoListId: number; value: CreateTodoListModel },
  {
    rejectValue: ApiError;
  }
>("todos/createTodoItem", async ({ todoListId, value }, thunkApi) => {
  const result = (await createTodoItemAsync(todoListId, value)) as any;
  if (result.error) {
    return thunkApi.rejectWithValue(result.error as ApiError);
  }
  return result as TodoItemModel;
});

export const deleteTodoItem = createAsyncThunk<
  number,
  number,
  {
    rejectValue: ApiError;
  }
>("todos/deleteTodoItem", async (todoItemId, thunkApi) => {
  const error = await deleteTodoItemAsync(todoItemId);
  if (error) {
    return thunkApi.rejectWithValue(error);
  }

  return todoItemId;
});

export const updateTodoItem = createAsyncThunk<
  TodoItemModel,
  TodoItemModel,
  {
    rejectValue: ApiError;
  }
>("todos/updateTodoItem", async (todoItem, thunkApi) => {
  const error = await updateTodoItemAsync(todoItem);
  if (error) {
    return thunkApi.rejectWithValue(error);
  }

  return todoItem;
});

const todosReducer = createSlice({
  name: "todos",
  initialState,
  reducers: {
    selectList(state, action: PayloadAction<number>) {
      state.selectedList = state.lists.find((l) => l.id === action.payload);
    },
  },
  extraReducers: (builder) => {
    builder
      .addCase(fetchTodoLists.pending, (state) => {
        state.listLoadingStatus = "loading";
      })
      .addCase(fetchTodoLists.fulfilled, (state, action) => {
        state.listLoadingStatus = "idle";
        state.lists = action.payload;
      })
      .addCase(fetchTodoLists.rejected, (state) => {
        state.listLoadingStatus = "failed";
      });

    builder
      .addCase(createTodoList.pending, (state) => {
        state.listSavingStatus = "loading";
      })
      .addCase(createTodoList.fulfilled, (state, action) => {
        state.listSavingStatus = "idle";
        if (action.payload) {
          state.lists.push(action.payload);
        }
      })
      .addCase(createTodoList.rejected, (state) => {
        state.listSavingStatus = "failed";
      });

    builder
      .addCase(deleteTodoList.pending, (state) => {
        state.listSavingStatus = "loading";
      })
      .addCase(deleteTodoList.fulfilled, (state, action) => {
        state.listSavingStatus = "idle";
        if (action.payload) {
          state.lists = state.lists.filter((l) => l.id !== action.payload);
          if (state.selectedList?.id === action.payload) {
            state.selectedList = undefined;
          }
        }
      })
      .addCase(deleteTodoList.rejected, (state) => {
        state.listSavingStatus = "failed";
      });

    builder
      .addCase(updateTodoList.pending, (state) => {
        state.listSavingStatus = "loading";
      })
      .addCase(updateTodoList.fulfilled, (state, action) => {
        state.listSavingStatus = "idle";
        if (action.payload) {
          const listIndex = state.lists.findIndex(
            (l) => l.id === action.payload.id,
          );
          if (listIndex !== -1) {
            state.lists[listIndex] = action.payload;
          }
          if (state.selectedList?.id === action.payload.id) {
            state.selectedList = action.payload;
          }
        }
      })
      .addCase(updateTodoList.rejected, (state) => {
        state.listSavingStatus = "failed";
      });

    builder
      .addCase(createTodoItem.pending, (state) => {
        state.itemSavingStatus = "loading";
      })
      .addCase(createTodoItem.fulfilled, (state, action) => {
        state.itemSavingStatus = "idle";
        const listIndex = state.lists.findIndex(
          (l) => l.id === action.payload.todoListId,
        );
        if (listIndex !== -1) {
          state.lists[listIndex].todos.push(action.payload);
          state.selectedList = state.lists[listIndex];
        }
      })
      .addCase(createTodoItem.rejected, (state) => {
        state.itemSavingStatus = "failed";
      });

    builder
      .addCase(deleteTodoItem.pending, (state) => {
        state.itemSavingStatus = "loading";
      })
      .addCase(deleteTodoItem.fulfilled, (state, action) => {
        state.itemSavingStatus = "idle";
        if (state.selectedList && action.payload) {
          state.selectedList.todos = state.selectedList.todos.filter(
            (i) => i.id !== action.payload,
          );
          const listIndex = state.lists.findIndex(
            (l) => l.id === state.selectedList?.id,
          );
          if (listIndex !== -1) {
            state.lists[listIndex] = state.selectedList;
          }
        }
      })
      .addCase(deleteTodoItem.rejected, (state) => {
        state.itemSavingStatus = "failed";
      });

    builder
      .addCase(updateTodoItem.pending, (state) => {
        state.itemSavingStatus = "loading";
      })
      .addCase(updateTodoItem.fulfilled, (state, action) => {
        state.itemSavingStatus = "idle";
        if (state.selectedList && action.payload) {
          const itemIndex = state.selectedList.todos.findIndex(
            (i) => i.id === action.payload.id,
          );
          if (itemIndex !== -1) {
            state.selectedList.todos[itemIndex] = action.payload;
            const listIndex = state.lists.findIndex(
              (l) => l.id === state.selectedList?.id,
            );
            if (listIndex !== -1) {
              state.lists[listIndex] = state.selectedList;
            }
          }
        }
      })
      .addCase(updateTodoItem.rejected, (state) => {
        state.itemSavingStatus = "failed";
      });
  },
});

export default todosReducer.reducer;
export const { selectList } = todosReducer.actions;
export const selectTodosState = (state: RootState) => state.todos;
