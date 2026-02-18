"use client";
import { useState } from "react";
import { useRouter } from "next/navigation";
import Modal from "./Modal";
import TaskForm from "./TaskForm";
import { updateTodo, deleteTodo } from "@/api";
import type { ITask } from "@/types/tasks";
import { formatDateUTC } from "@/app/lib/date";
import {
  useUpdateTask,
  useDeleteTask,
} from "@/lib/queries/useTodos";

interface TaskProps {
  task: ITask;
  onUpdated?: (task: ITask) => void;
  onDeleted?: (id: string) => void;
}

const Task: React.FC<TaskProps> = ({ task, onUpdated, onDeleted }) => {
  const [editOpen, setEditOpen] = useState(false);

  const updateMutation = useUpdateTask();
  const deleteMutation = useDeleteTask();

  const handleDelete = () => {
    if (!confirm("Delete this task?")) return;
    deleteMutation.mutate(task.id, {
      onSuccess: () => {
        // 缓存会通过 hooks 内部的 invalidate 生效
        onDeleted?.(task.id); // 如果父组件传了回调，继续调用
      },
    });
  };

  return (
    <>
      <tr>
        <td>{task.title}</td>
        <td className="break-words">{task.description}</td>
        <td>
          {task.category ? (
            <div 
              className="badge text-white border-none" 
              style={{ backgroundColor: task.category.color }}
            >
              {task.category.name}
            </div>
          ) : (
            <span className="text-gray-400 italic">No category</span>
          )}
        </td>
        <td>{task.completed ? "Yes" : "No"}</td>
        <td>
          <time dateTime={task.createdAt}>{formatDateUTC(task.createdAt)}</time>
        </td>
        <td>
          {task.updatedAt ? (
            <time dateTime={task.updatedAt}>
              {formatDateUTC(task.updatedAt)}
            </time>
          ) : (
            "N/A"
          )}
        </td>
        <td>
          <button
            className="btn btn-sm"
            onClick={() => setEditOpen(true)}
            disabled={deleteMutation.isPending}
          >
            Update
          </button>
        </td>
        <td>
          <button
            className="btn btn-sm btn-error"
            onClick={handleDelete}
            disabled={updateMutation.isPending || deleteMutation.isPending}
          >
            Delete
          </button>
        </td>
      </tr>

      <Modal modalOpen={editOpen} setModalOpen={setEditOpen}>
        <TaskForm
          mode="update"
          initial={{
            title: task.title,
            description: task.description,
            completed: task.completed,
          }}
          // ✅ 直接用 mutation 的 isPending 作为提交 loading
          submitting={updateMutation.isPending}
          onSubmit={(v) => {
            updateMutation.mutate(
              { id: task.id, updates: v },
              {
                onSuccess: (updated) => {
                  onUpdated?.(updated); // 有回调就通知父组件
                  setEditOpen(false);
                },
              }
            );
          }}
        />
      </Modal>
    </>
  );
};

export default Task;