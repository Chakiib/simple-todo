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

import { CreateTodoListModel } from "../models/TodoModels";

interface DialogNewTodoListProps {
  open: boolean;
  onClose: (todolist?: CreateTodoListModel) => void;
}

const DialogNewTodoList: React.FC<DialogNewTodoListProps> = ({
  open,
  onClose,
}) => {
  const [listName, setListName] = React.useState<string>("");

  const handleNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event && event.target) {
      setListName(event.target.value);
    }
  };

  const handleCancel = () => {
    onClose();
  };
  const handleCreate = () => {
    onClose({ name: listName });
  };

  return (
    <Dialog open={open} onClose={handleCancel} fullWidth maxWidth="sm">
      <DialogTitle>Create new a TODO list</DialogTitle>
      <DialogContent>
        <DialogContentText>Please enter the list name.</DialogContentText>
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

export default DialogNewTodoList;
