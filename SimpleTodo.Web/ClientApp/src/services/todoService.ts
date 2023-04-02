import { ApiError } from "../models/ApiModels";
import {
  CreateTodoListModel,
  TodoItemModel,
  TodoListModel,
} from "../models/TodoModels";

const TODO_ROUTE = "api/todos";
const TODOLIST_ROUTE = "api/todolists";

const defaultHeaders = { Accept: "application/json" };

// function getRandomInt(min: number, max: number): number {
//   return Math.floor(Math.random() * (max - min) + min);
// }

// function createPromise(
//   data: any,
//   latencyOptions = {
//     minMs: 0,
//     maxMs: 5000,
//     shouldFail: false,
//   },
// ): Promise<any> {
//   return new Promise((resolve, reject) => {
//     setTimeout(
//       latencyOptions.shouldFail === true ? reject : resolve,
//       latencyOptions.minMs === latencyOptions.maxMs
//         ? latencyOptions.minMs
//         : getRandomInt(latencyOptions.minMs, latencyOptions.maxMs),
//       data,
//     );
//   });
// }

const handleError = (error: any, httpStatus?: number): ApiError => {
  if (!error) {
    return {
      message: "unknown error",
      httpStatus,
      data: error,
    };
  }
  if (typeof error === "string") {
    return {
      message: error,
      httpStatus,
    };
  }
  if (typeof error === "object") {
    const { name, message, stack, code, status, ...data } = error;
    return {
      name,
      message,
      stack,
      code,
      httpStatus: httpStatus || status,
      data,
    };
  } else {
    return {
      message: "unknown error",
      httpStatus,
      data: error,
    };
  }
};

const getTodoListsAsync = async (): Promise<
  TodoListModel[] | { error: ApiError }
> => {
  try {
    const result = await fetch(TODOLIST_ROUTE, {
      method: "GET",
      headers: { ...defaultHeaders },
    });

    if (!result.ok) {
      return { error: handleError(await result.json(), result.status) };
    }

    return result.json() as Promise<TodoListModel[]>;
  } catch (e) {
    return { error: handleError(e) };
  }
};

const createTodoListAsync = async (
  todoList: CreateTodoListModel,
): Promise<TodoListModel | { error: ApiError }> => {
  try {
    const result = await fetch(TODOLIST_ROUTE, {
      method: "POST",
      headers: { ...defaultHeaders, "Content-type": "application/json" },
      body: JSON.stringify(todoList),
    });

    if (!result.ok) {
      return { error: handleError(await result.json(), result.status) };
    }

    return result.json() as Promise<TodoListModel>;
  } catch (error) {
    return { error: handleError(error) };
  }
};

const deleteTodoListsAsync = async (
  todoListId: number,
): Promise<null | ApiError> => {
  try {
    const result = await fetch(`${TODOLIST_ROUTE}/${todoListId}`, {
      method: "DELETE",
      headers: { ...defaultHeaders },
    });

    return result.ok ? null : handleError(await result.json(), result.status);
  } catch (e) {
    return handleError(e);
  }
};

const updateTodoListsAsync = async (
  todoList: TodoListModel,
): Promise<null | ApiError> => {
  try {
    const result = await fetch(`${TODOLIST_ROUTE}/${todoList.id}`, {
      method: "PUT",
      headers: { ...defaultHeaders, "Content-type": "application/json" },
      body: JSON.stringify(todoList),
    });

    return result.ok ? null : handleError(await result.json(), result.status);
  } catch (e) {
    return handleError(e);
  }
};

export {
  getTodoListsAsync,
  createTodoListAsync,
  deleteTodoListsAsync,
  updateTodoListsAsync,
};
