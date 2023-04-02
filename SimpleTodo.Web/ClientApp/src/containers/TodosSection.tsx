import React from "react";
import { Button, IconButton, Typography } from "@mui/material";
import Grid from "@mui/material/Unstable_Grid2/Grid2";
import EditIcon from "@mui/icons-material/Edit";

import { useAppSelector, useAppDispatch } from "../app/hooks";
import { selectTodosState, updateTodoList } from "../slices/todosSlice";
import DialogEditTodoList from "../components/DialogEditTodoList";
import { TodoListModel } from "../models/TodoModels";

const TodosSection = () => {
  const { selectedList } = useAppSelector(selectTodosState);
  const dispatch = useAppDispatch();

  const [dialogEditListOpen, setDialogEditListOpen] =
    React.useState<boolean>(false);

  const handleEditClick = () => {
    setDialogEditListOpen(true);
  };

  const handleDialogEditListClose = (todoList?: TodoListModel) => {
    setDialogEditListOpen(false);
    if (todoList) {
      dispatch(updateTodoList(todoList));
    }
  };

  return (
    <>
      <Grid container>
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
            {selectedList?.todos.map((t) => (
              <Typography>{t.name}</Typography>
            ))}
          </Grid>
          <Grid>
            <IconButton
              edge="end"
              aria-label="edit"
              disabled={selectedList === undefined}
              onClick={handleEditClick}
            >
              <EditIcon />
            </IconButton>
          </Grid>
        </Grid>

        <Button variant="contained">Add a new todo</Button>
      </Grid>
      <DialogEditTodoList
        open={dialogEditListOpen}
        value={selectedList}
        onClose={handleDialogEditListClose}
      />
    </>
  );
};

export default TodosSection;
