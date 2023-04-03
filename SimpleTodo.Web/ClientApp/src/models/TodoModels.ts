export interface TodoItemModel {
  id: number;
  name: string;
  description: string;
  isComplete: boolean;
  todoListId: number;
}

export interface TodoListModel {
  id: number;
  name: string;
  todos: TodoItemModel[];
}

export interface CreateTodoListModel {
  name: string;
}

export interface CreateTodoItemModel {
  name: string;
  description?: string;
}
