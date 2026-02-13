import type { ITask } from "./types/tasks";

const baseUrl = "http://localhost:5133/api";

export const getAllTodos = async (): Promise<ITask[]> => {
  const res = await fetch(`${baseUrl}/Todo`, {
    cache: "no-store",
  });
  if (!res.ok) throw new Error(`GET /Todo failed: ${res.status}`);
  return res.json();
};

export const addNewTodo = async (
  title: string,
  description?: string,
): Promise<ITask> => {
  const newTask = {
    title,
    description: description?.trim() || null,
    completed: false,
  };
  const res = await fetch(`${baseUrl}/Todo`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(newTask),
  });
  if (!res.ok) throw new Error(`POST /Todo failed: ${res.status}`);
  return res.json();
};

export const updateTodo = async (
  id: string,
  updates: Partial<ITask>,
): Promise<ITask> => {
  // .NET backend expects the whole object or a specific DTO for PUT
  // For simplicity, let's assume we can fetch the current one and merge,
  // but here we just send what we have.
  // Note: The backend uses PUT /api/Todo/{id}
  
  // First get the current item to ensure we have all fields for PUT
  const currentRes = await fetch(`${baseUrl}/Todo/${id}`);
  if (!currentRes.ok) throw new Error(`GET /Todo/${id} failed`);
  const current = await currentRes.json();
  
  const updatedItem = { ...current, ...updates };

  const res = await fetch(`${baseUrl}/Todo/${id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(updatedItem),
  });
  if (!res.ok) throw new Error(`PUT /Todo/${id} failed: ${res.status}`);
  
  // PUT normally returns 204 No Content in this controller
  if (res.status === 204) {
    return updatedItem;
  }
  return res.json();
};

export const deleteTodo = async (id: string): Promise<void> => {
  const res = await fetch(`${baseUrl}/Todo/${id}`, { method: "DELETE" });
  if (!res.ok) throw new Error(`DELETE /Todo/${id} failed: ${res.status}`);
};
