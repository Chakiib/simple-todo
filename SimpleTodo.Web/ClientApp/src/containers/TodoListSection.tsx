import React from "react";
import {
  Alert,
  Button,
  IconButton,
  List,
  ListItem,
  ListItemButton,
  ListItemText,
  Skeleton,
  Snackbar,
  TextField,
  Typography,
} from "@mui/material";
import Grid from "@mui/material/Unstable_Grid2/Grid2";
import DeleteIcon from "@mui/icons-material/Delete";
import { useAppSelector, useAppDispatch } from "../app/hooks";
import {
  selectTodosState,
  fetchTodoLists,
  createTodoList,
  selectList,
  deleteTodoList,
} from "../slices/todosSlice";
import { CreateTodoListModel } from "../models/TodoModels";
import DialogNewTodoList from "../components/DialogTodoList";

const renderLoadingSkeleton = (amount: number) => {
  return Array.from(Array(amount), (_, index) => (
    <ListItem key={index + 1}>
      <Skeleton variant="rectangular" width="100%" height={48} />
    </ListItem>
  ));
};

const TodoListSection = () => {
  const { lists, listLoadingStatus, listSavingStatus } =
    useAppSelector(selectTodosState);
  const dispatch = useAppDispatch();

  const [dialogNewListOpen, setDialogNewListOpen] =
    React.useState<boolean>(false);
  const [successToast, setSuccessToast] = React.useState({
    open: false,
    message: "",
  });
  const [errorToast, setErrorToast] = React.useState({
    open: false,
    message: "",
  });
  const [listNameFilter, setListNameFilter] = React.useState<string>();

  React.useEffect(() => {
    dispatch(fetchTodoLists());
  }, [dispatch]);

  const handleCreateClick = () => {
    setDialogNewListOpen(true);
  };

  const handleListNameFilterchange = (
    event: React.ChangeEvent<HTMLInputElement>,
  ) => {
    if (event && event.target) {
      setListNameFilter(event.target.value);
    }
  };

  const handleSelectClick = (todoListId: number) => () => {
    dispatch(selectList(todoListId));
  };

  const handleDeleteClick = (todoListId?: number) => async () => {
    if (!todoListId) {
      return;
    }
    const resultAction = await dispatch(deleteTodoList(todoListId));

    if (createTodoList.rejected.match(resultAction)) {
      setErrorToast({
        open: true,
        message: "The list could not be removed. Please retry.",
      });
    } else {
      setSuccessToast({ open: true, message: "The list has been removed." });
    }
  };

  const handleDialogNewListClose = async (todoList?: CreateTodoListModel) => {
    setDialogNewListOpen(false);
    if (!todoList) {
      return;
    }

    const resultAction = await dispatch(createTodoList(todoList));

    if (createTodoList.rejected.match(resultAction)) {
      setErrorToast({
        open: true,
        message: "The new list could not be created. Please retry.",
      });
    } else {
      setSuccessToast({
        open: true,
        message: "The new list has been created.",
      });
    }
  };

  const handleToastClose = () => {
    if (errorToast.open)
      setErrorToast((s) => ({ open: false, message: s.message }));
    if (successToast.open)
      setSuccessToast((s) => ({ open: false, message: s.message }));
  };

  const isLoading =
    listLoadingStatus === "loading" || listSavingStatus === "loading";

  const todoLists = listNameFilter
    ? lists.filter((l) => l.name.toLowerCase().includes(listNameFilter))
    : lists;

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
            <Button
              variant="contained"
              disabled={isLoading}
              onClick={handleCreateClick}
            >
              Create new list
            </Button>
          </Grid>
          <Grid>
            <TextField
              id="search-todolist"
              label="Search a list"
              variant="outlined"
              disabled={isLoading}
              value={listNameFilter}
              onChange={handleListNameFilterchange}
            />
          </Grid>
        </Grid>
        <Grid xs={12}>
          <List>
            {listLoadingStatus === "loading" && renderLoadingSkeleton(3)}
            {listLoadingStatus === "failed" && (
              <Typography align="center">
                Something went wrong. Please retry.
              </Typography>
            )}
            {listLoadingStatus === "idle" &&
              todoLists.map((l) => (
                <ListItem
                  key={l.id}
                  secondaryAction={
                    <>
                      <IconButton
                        edge="end"
                        aria-label="delete"
                        onClick={handleDeleteClick(l.id)}
                      >
                        <DeleteIcon />
                      </IconButton>
                    </>
                  }
                  disablePadding
                >
                  <ListItemButton onClick={handleSelectClick(l.id)}>
                    <ListItemText primary={l.name} />
                  </ListItemButton>
                </ListItem>
              ))}
            {listSavingStatus === "loading" && renderLoadingSkeleton(1)}
          </List>
        </Grid>
      </Grid>
      <DialogNewTodoList
        open={dialogNewListOpen}
        onClose={handleDialogNewListClose}
      />

      <Snackbar
        open={successToast.open}
        autoHideDuration={6000}
        onClose={handleToastClose}
      >
        <Alert
          onClose={handleToastClose}
          severity="success"
          sx={{ width: "100%" }}
        >
          {successToast.message}
        </Alert>
      </Snackbar>
      <Snackbar
        open={errorToast.open}
        autoHideDuration={6000}
        onClose={handleToastClose}
      >
        <Alert
          onClose={handleToastClose}
          severity="error"
          sx={{ width: "100%" }}
        >
          {errorToast.message}
        </Alert>
      </Snackbar>
    </>
  );
};

export default TodoListSection;
