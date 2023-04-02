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

import { CreateTodoItemModel } from "../models/TodoModels";

interface DialogNewTodoItemProps {
  open: boolean;
  onClose: (todoItem?: CreateTodoItemModel) => void;
}

const DialogNewTodoItem: React.FC<DialogNewTodoItemProps> = ({
  open,
  onClose,
}) => {
  const [itemName, setItemName] = React.useState<string>("");

  const handleNameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    if (event && event.target) {
      setItemName(event.target.value);
    }
  };

  const handleCancel = () => {
    onClose();
  };
  const handleCreate = () => {
    onClose({ name: itemName });
  };

  return (
    <Dialog open={open} onClose={handleCancel} fullWidth maxWidth="sm">
      <DialogTitle>Add a new TODO</DialogTitle>
      <DialogContent>
        <DialogContentText>Please enter the name.</DialogContentText>
        <TextField
          id="item-name"
          label="Name"
          margin="dense"
          fullWidth
          autoFocus
          onChange={handleNameChange}
          value={itemName}
        />
      </DialogContent>
      <DialogActions>
        <Button onClick={handleCancel}>Cancel</Button>
        <Button onClick={handleCreate} disabled={itemName.length === 0}>
          Create
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default DialogNewTodoItem;
