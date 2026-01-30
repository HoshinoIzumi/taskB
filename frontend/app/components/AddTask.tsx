"use client";

import { GoPlus } from "react-icons/go";
import Modal from "./Modal";
import { useRouter } from "next/navigation";
import { useState } from "react";
import type { ITask } from "@/types/tasks";

// ✅ 新：使用 TanStack Query 封装的 hooks
import { useAddTask } from "@/lib/queries/useTodos";

interface AddTaskProps {
  onAdded?: (task: ITask) => void;
}

const AddTask: React.FC<AddTaskProps> = ({ onAdded }) => {
  const router = useRouter();
  const [modalOpen, setModalOpen] = useState(false);
  const [newTaskValue, setNewTaskValue] = useState("");
  const [description, setDescription] = useState("");

  const addMutation = useAddTask(); // 提供 mutate、isPending、error 等

  const handleSubmitNewTodo = (e: React.FormEvent) => {
    e.preventDefault();
    const title = newTaskValue.trim();
    if (!title) return;

    addMutation.mutate(
      { title, description },
      {
        onSuccess: (saved) => {
          onAdded?.(saved);           
          setNewTaskValue("");        
          setDescription("");
          setModalOpen(false);
          router.refresh();
        },
        onError: (err) => {
          console.error("Add task failed:", err);
        },
      }
    );
  };

  return (
    <div>
      <button
        onClick={() => setModalOpen(true)}
        className="btn btn-primary w-full"
      >
        <GoPlus size={20} className="ml-2" />
        Add new task
      </button>

      <Modal modalOpen={modalOpen} setModalOpen={setModalOpen}>
        <form onSubmit={handleSubmitNewTodo}>
          <h3 className="font-bold text-lg mb-4">Add New Task</h3>

          <input
            value={newTaskValue}
            onChange={(e) => setNewTaskValue(e.target.value)}
            type="text"
            placeholder="Task Title"
            className="input input-bordered w-full mb-4"
            autoFocus
          />

          <textarea
            value={description}
            onChange={(e) => setDescription(e.target.value)}
            placeholder="Task Description"
            className="textarea textarea-bordered w-full mb-4"
          />

          <button
            type="submit"
            className="btn btn-primary w-full"
            disabled={!newTaskValue.trim() || addMutation.isPending}
          >
            {addMutation.isPending ? "Saving..." : "Add Task"}
          </button>
        </form>
      </Modal>
    </div>
  );
};

export default AddTask;
