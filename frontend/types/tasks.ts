export interface ICategory {
  id: string;
  name: string;
  description?: string;
  color: string;
}

export interface ITask {
  id: string;
  title: string;
  description?: string;
  completed: boolean;
  createdAt: string;
  updatedAt?: string;
  categoryId: string;
  category: ICategory;
}
