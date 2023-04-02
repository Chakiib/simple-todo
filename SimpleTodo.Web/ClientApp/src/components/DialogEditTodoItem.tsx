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

import { TodoItemModel } from "../models/TodoModels";

interface DialogEditTodoItemProps {
  open: boolean;
  value?: TodoItemModel;
  onClose: (todoItem?: TodoItemModel) => void;
}

const DialogEditTodoItem: React.FC<DialogEditTodoItemProps> = ({
  open,
  value,
  onClose,
}) => {
  const [newName, setNewName] = React.useState<string>(value?.name || "");

  React.useEffect(() => {
    if (!open && value) {
      setNewName(value.name);
    }
  }, [value, open]);

  const handleNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event && event.target) {
      setNewName(event.target.value);
    }
  };

  const handleCancel = () => {
    onClose();
  };
  const handleCreate = () => {
    if (value) {
      onClose({ ...value, name: newName });
    }
  };

  return (
    <Dialog open={open} onClose={handleCancel} fullWidth maxWidth="sm">
      <DialogTitle>Edit TODO name</DialogTitle>
      <DialogContent>
        <DialogContentText>Please enter the new name.</DialogContentText>
        <TextField
          id="list-name"
          label="Name"
          margin="dense"
          fullWidth
          autoFocus
          onChange={handleNameChange}
          value={newName}
        />
      </DialogContent>
      <DialogActions>
        <Button onClick={handleCancel}>Cancel</Button>
        <Button onClick={handleCreate} disabled={newName.length === 0}>
          Create
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default DialogEditTodoItem;
