// lib/queries/useTodos.ts
"use client";
import { useQuery, useMutation, useQueryClient } from "@tanstack/react-query";
import { getAllTodos, getCategories, addNewTodo, updateTodo, deleteTodo } from "@/api"; 
import type { ITask, ICategory } from "@/types/tasks";

const key = ["tasks"]; 
const categoriesKey = ["categories"];

export function useTasks() {
  return useQuery<ITask[]>({
    queryKey: key,
    queryFn: getAllTodos,
  });
}

export function useCategories() {
  return useQuery<ICategory[]>({
    queryKey: categoriesKey,
    queryFn: getCategories,
  });
}

export function useAddTask() {
  const qc = useQueryClient();
  return useMutation({
    mutationFn: (vars: { title: string; categoryId: string; description?: string }) =>
      addNewTodo(vars.title, vars.categoryId, vars.description),
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
