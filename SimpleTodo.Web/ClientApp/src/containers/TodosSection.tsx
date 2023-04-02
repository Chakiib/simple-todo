import React from "react";
import { Button, IconButton, List, TextField, Typography } from "@mui/material";
import Grid from "@mui/material/Unstable_Grid2/Grid2";
import EditIcon from "@mui/icons-material/Edit";

import { useAppSelector, useAppDispatch } from "../app/hooks";
import {
  createTodoItem,
  deleteTodoItem,
  selectTodosState,
  updateTodoItem,
  updateTodoList,
} from "../slices/todosSlice";
import {
  CreateTodoItemModel,
  TodoItemModel,
  TodoListModel,
} from "../models/TodoModels";
import DialogEditTodoList from "../components/DialogEditTodoList";
import DialogEditTodoItem from "../components/DialogEditTodoItem";
import DialogNewTodoItem from "../components/DialogNewTodoItem";
import TodoItem from "../components/TodoItem";

const TodosSection = () => {
  const { selectedList, itemSavingStatus } = useAppSelector(selectTodosState);
  const dispatch = useAppDispatch();

  const [currentEditItem, setEditItem] = React.useState<TodoItemModel>();
  const [itemNameFilter, setItemNameFilter] = React.useState<string>();

  const [dialogNewTodoOpen, setDialogNewTodoOpen] =
    React.useState<boolean>(false);
  const [dialogEditListOpen, setDialogEditListOpen] =
    React.useState<boolean>(false);
  const [dialogEditOpen, setDialogEditOpen] = React.useState<boolean>(false);

  const handleCreateClick = () => {
    setDialogNewTodoOpen(true);
  };

  const handleListNameEditClick = () => {
    setDialogEditListOpen(true);
  };

  const handleItemNameFilterchange = (
    event: React.ChangeEvent<HTMLInputElement>,
  ) => {
    if (event && event.target) {
      setItemNameFilter(event.target.value);
    }
  };

  const handleCompleteClick = (todoItem: TodoItemModel) => {
    dispatch(updateTodoItem({ ...todoItem, isComplete: !todoItem.isComplete }));
  };

  const handleEditClick = (todoItem: TodoItemModel) => {
    setEditItem(todoItem);
    setDialogEditOpen(true);
  };

  const handleDeleteClick = (todoItemId: number) => {
    dispatch(deleteTodoItem(todoItemId));
  };

  const handleDialogNewTodoClose = (todoItem?: CreateTodoItemModel) => {
    setDialogNewTodoOpen(false);
    if (selectedList && todoItem) {
      dispatch(
        createTodoItem({ todoListId: selectedList.id, value: todoItem }),
      );
    }
  };

  const handleDialogEditListClose = (todoList?: TodoListModel) => {
    setDialogEditListOpen(false);
    if (todoList) {
      dispatch(updateTodoList(todoList));
    }
  };

  const handleDialogEditClose = (todoItem?: TodoItemModel) => {
    setDialogEditOpen(false);
    if (todoItem) {
      dispatch(updateTodoItem(todoItem));
    }
  };

  const isLoading = itemSavingStatus === "loading";

  const todos = itemNameFilter
    ? (selectedList?.todos || []).filter((t) =>
        t.name.toLowerCase().includes(itemNameFilter),
      )
    : selectedList?.todos || [];

  return (
    <>
      <Grid container rowSpacing={2}>
        <Grid
          container
          xs={12}
          justifyContent="space-between"
          alignItems="center"
        >
          <Grid>
            <Typography>
              {selectedList ? selectedList.name : "Select a list"}
            </Typography>
          </Grid>
          <Grid>
            <IconButton
              edge="end"
              aria-label="edit"
              disabled={selectedList === undefined}
              onClick={handleListNameEditClick}
            >
              <EditIcon />
            </IconButton>
          </Grid>
        </Grid>
        <Grid
          container
          xs={12}
          justifyContent="space-between"
          alignItems="center"
        >
          <Grid>
            <Button
              variant="contained"
              disabled={isLoading}
              onClick={handleCreateClick}
            >
              Add a new todo
            </Button>
          </Grid>
          <Grid>
            <TextField
              id="search-todoitem"
              label="Search a todo"
              variant="outlined"
              disabled={isLoading}
              value={itemNameFilter}
              onChange={handleItemNameFilterchange}
            />
          </Grid>
        </Grid>
        <Grid xs={12}>
          <List>
            {todos.map((l) => (
              <TodoItem
                key={l.id}
                item={l}
                onComplete={handleCompleteClick}
                onEdit={handleEditClick}
                onDelete={handleDeleteClick}
              />
            ))}
          </List>
        </Grid>
      </Grid>
      <DialogNewTodoItem
        open={dialogNewTodoOpen}
        onClose={handleDialogNewTodoClose}
      />
      <DialogEditTodoList
        open={dialogEditListOpen}
        value={selectedList}
        onClose={handleDialogEditListClose}
      />
      <DialogEditTodoItem
        open={dialogEditOpen}
        value={currentEditItem}
        onClose={handleDialogEditClose}
      />
    </>
  );
};

export default TodosSection;
