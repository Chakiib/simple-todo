import React from "react";
import {
  Checkbox,
  IconButton,
  ListItem,
  ListItemButton,
  ListItemIcon,
  ListItemText,
} from "@mui/material";
import EditIcon from "@mui/icons-material/Edit";
import DeleteIcon from "@mui/icons-material/Delete";

import { TodoItemModel } from "../models/TodoModels";

export interface TodoItemProps {
  item: TodoItemModel;
  onComplete: (id: TodoItemModel) => void;
  onEdit: (item: TodoItemModel) => void;
  onDelete: (id: number) => void;
}

const TodoItem: React.FC<TodoItemProps> = ({
  item,
  onComplete,
  onEdit,
  onDelete,
}) => {
  const handleCompleteClick = React.useCallback(() => {
    onComplete(item);
  }, [item, onComplete]);

  const handleEditClick = React.useCallback(() => {
    onEdit(item);
  }, [item, onEdit]);

  const handleDeleteClick = React.useCallback(() => {
    onDelete(item.id);
  }, [item.id, onDelete]);

  return (
    <ListItem
      key={item.id}
      secondaryAction={
        <>
          <IconButton edge="end" aria-label="edit" onClick={handleEditClick}>
            <EditIcon />
          </IconButton>
          <IconButton
            edge="end"
            aria-label="delete"
            onClick={handleDeleteClick}
          >
            <DeleteIcon />
          </IconButton>
        </>
      }
      disablePadding
    >
      <ListItemButton onClick={handleCompleteClick} dense>
        <ListItemIcon>
          <Checkbox edge="start" checked={item.isComplete} disableRipple />
        </ListItemIcon>
        <ListItemText primary={item.name} />
      </ListItemButton>
    </ListItem>
  );
};

export default TodoItem;
