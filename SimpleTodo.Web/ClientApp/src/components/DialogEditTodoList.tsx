import React from "react";
import {
  Button,
  Dialog,
  DialogTitle,
  DialogContent,
  DialogContentText,
  DialogActions,
  TextField,
} from "@mui/material";

import { TodoListModel } from "../models/TodoModels";

interface DialogEditTodoListProps {
  open: boolean;
  value?: TodoListModel;
  onClose: (todolist?: TodoListModel) => void;
}

const DialogEditTodoList: React.FC<DialogEditTodoListProps> = ({
  open,
  value,
  onClose,
}) => {
  const [listName, setListName] = React.useState<string>(value?.name || "");

  const handleNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event && event.target) {
      setListName(event.target.value);
    }
  };

  const handleCancel = () => {
    onClose();
  };
  const handleCreate = () => {
    if (value) {
      onClose({ ...value, name: listName });
    }
  };

  return (
    <Dialog open={open} onClose={handleCancel} fullWidth maxWidth="sm">
      <DialogTitle>Edit TODO list name</DialogTitle>
      <DialogContent>
        <DialogContentText>Please enter the new list name.</DialogContentText>
        <TextField
          id="list-name"
          label="Name"
          margin="dense"
          fullWidth
          autoFocus
          onChange={handleNameChange}
          value={listName}
        />
      </DialogContent>
      <DialogActions>
        <Button onClick={handleCancel}>Cancel</Button>
        <Button onClick={handleCreate}>Create</Button>
      </DialogActions>
    </Dialog>
  );
};

export default DialogEditTodoList;
