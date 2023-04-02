import React from "react";
import { Container, Stack, Typography } from "@mui/material";
import TodoListSection from "./containers/TodoListSection";
import TodosSection from "./containers/TodosSection";

function App() {
  return (
    <Container maxWidth="sm">
      <Stack spacing={2}>
        <Typography variant="h4">Simple Todo</Typography>
        <TodoListSection />
        <TodosSection />
      </Stack>
    </Container>
  );
}

export default App;
