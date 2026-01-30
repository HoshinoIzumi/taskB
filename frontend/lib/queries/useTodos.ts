// lib/queries/useTodos.ts
"use client";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { getAllTodos, addNewTodo, updateTodo, deleteTodo } from "@/api"; 
import type { ITask } from "@/types/tasks";

const key = ["tasks"]; 

export function useTasks() {
  return useQuery<ITask[]>({
    queryKey: key,
    queryFn: getAllTodos,
    // 可按需：staleTime: 10_000, gcTime: 5 * 60_000
  });
}

export function useAddTask() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (vars: { title: string; description?: string }) =>
      addNewTodo(vars.title, vars.description),
    onSuccess: () => qc.invalidateQueries({ queryKey: key }), 
  });
}

export function useUpdateTask() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (vars: { id: string; updates: Partial<ITask> }) =>
      updateTodo(vars.id, vars.updates),
    onSuccess: () => qc.invalidateQueries({ queryKey: key }),
  });
}

export function useDeleteTask() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (id: string) => deleteTodo(id),
    onSuccess: () => qc.invalidateQueries({ queryKey: key }),
  });
}
