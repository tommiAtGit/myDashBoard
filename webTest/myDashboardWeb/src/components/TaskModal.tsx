import React, { useState, useEffect } from 'react';
import { Task, TaskStatus } from '../types';
import { generateId } from '../utils/helpers';

interface TaskModalProps {
  isOpen: boolean;
  task?: Task;
  onClose: () => void;
  onSave: (task: Task) => void;
}

const TaskModal: React.FC<TaskModalProps> = ({ isOpen, task, onClose, onSave }) => {
  const [formData, setFormData] = useState<Partial<Task>>({
    title: '',
    description: '',
    owner: '',
    status: TaskStatus.OPEN
  });

  useEffect(() => {
    if (task) {
      setFormData(task);
    } else {
      setFormData({
        title: '',
        description: '',
        owner: '',
        status: TaskStatus.OPEN
      });
    }
  }, [task, isOpen]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    
    const newTask: Task = {
      id: task?.id || generateId(),
      title: formData.title || '',
      description: formData.description || '',
      owner: formData.owner || '',
      status: formData.status || TaskStatus.OPEN,
      createdAt: task?.createdAt || new Date(),
      updatedAt: new Date()
    };

    onSave(newTask);
    onClose();
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
  };

  if (!isOpen) return null;

  return (
    <div className="modal-overlay" onClick={onClose}>
      <div className="modal-content" onClick={(e) => e.stopPropagation()}>
        <h2 className="modal-header">Task</h2>
        <form className="modal-form" onSubmit={handleSubmit}>
          <div className="form-group">
            <label className="form-label">Task Title</label>
            <input
              type="text"
              name="title"
              value={formData.title}
              onChange={handleChange}
              className="form-input"
              placeholder="Enter task title"
              required
            />
          </div>

          <div className="form-group">
            <label className="form-label">Task Description</label>
            <textarea
              name="description"
              value={formData.description}
              onChange={handleChange}
              className="form-textarea"
              placeholder="Enter task description"
              required
            />
          </div>

          <div className="form-group">
            <label className="form-label">Task Owner</label>
            <input
              type="text"
              name="owner"
              value={formData.owner}
              onChange={handleChange}
              className="form-input"
              placeholder="Enter owner name"
              required
            />
          </div>

          <div className="form-group">
            <label className="form-label">Task Status</label>
            <select
              name="status"
              value={formData.status}
              onChange={handleChange}
              className="form-select"
              required
            >
              <option value={TaskStatus.OPEN}>Open</option>
              <option value={TaskStatus.IN_PROGRESS}>In Progress</option>
              <option value={TaskStatus.DONE}>Done</option>
            </select>
          </div>

          <div className="modal-actions">
            <button type="button" className="cancel-button" onClick={onClose}>
              Cancel
            </button>
            <button type="submit" className="save-button">
              Save
            </button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default TaskModal;
