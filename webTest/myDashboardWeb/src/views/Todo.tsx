import React, { useState, useEffect } from 'react';
import { DragDropContext, Droppable, Draggable, DropResult } from 'react-beautiful-dnd';
import { Task, TaskStatus, TaskColumn } from '../types';
import { loadTasks, saveTasks } from '../utils/helpers';
import TaskModal from '../components/TaskModal';

const Todo: React.FC = () => {
  const [tasks, setTasks] = useState<Task[]>([]);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [editingTask, setEditingTask] = useState<Task | undefined>(undefined);

  useEffect(() => {
    const loadedTasks = loadTasks();
    setTasks(loadedTasks);
  }, []);

  useEffect(() => {
    saveTasks(tasks);
  }, [tasks]);

  const columns: TaskColumn[] = [
    {
      id: TaskStatus.OPEN,
      title: 'Open Tasks',
      tasks: tasks.filter(task => task.status === TaskStatus.OPEN)
    },
    {
      id: TaskStatus.IN_PROGRESS,
      title: 'In Progress Tasks',
      tasks: tasks.filter(task => task.status === TaskStatus.IN_PROGRESS)
    },
    {
      id: TaskStatus.DONE,
      title: 'Done Tasks',
      tasks: tasks.filter(task => task.status === TaskStatus.DONE)
    }
  ];

  const handleDragEnd = (result: DropResult) => {
    const { source, destination, draggableId } = result;

    if (!destination) return;
    if (source.droppableId === destination.droppableId && source.index === destination.index) return;

    const newStatus = destination.droppableId as TaskStatus;
    
    setTasks(prevTasks => 
      prevTasks.map(task => 
        task.id === draggableId 
          ? { ...task, status: newStatus, updatedAt: new Date() }
          : task
      )
    );
  };

  const handleNewTask = () => {
    setEditingTask(undefined);
    setIsModalOpen(true);
  };

  const handleEditTask = (task: Task) => {
    setEditingTask(task);
    setIsModalOpen(true);
  };

  const handleDeleteTask = (taskId: string) => {
    setTasks(prevTasks => prevTasks.filter(task => task.id !== taskId));
  };

  const handleSaveTask = (task: Task) => {
    setTasks(prevTasks => {
      const existingIndex = prevTasks.findIndex(t => t.id === task.id);
      if (existingIndex >= 0) {
        const newTasks = [...prevTasks];
        newTasks[existingIndex] = task;
        return newTasks;
      }
      return [...prevTasks, task];
    });
  };

  return (
    <div className="todo-container">
      <div className="todo-header">
        <button className="new-task-button" onClick={handleNewTask}>
          New Task
        </button>
      </div>

      <DragDropContext onDragEnd={handleDragEnd}>
        <div className="todo-board">
          {columns.map(column => (
            <div key={column.id} className="task-column">
              <h3 className="column-header">{column.title}</h3>
              <Droppable droppableId={column.id}>
                {(provided) => (
                  <div
                    className="task-list"
                    ref={provided.innerRef}
                    {...provided.droppableProps}
                  >
                    {column.tasks.map((task, index) => (
                      <Draggable key={task.id} draggableId={task.id} index={index}>
                        {(provided, snapshot) => (
                          <div
                            ref={provided.innerRef}
                            {...provided.draggableProps}
                            {...provided.dragHandleProps}
                            className={`task-card ${snapshot.isDragging ? 'dragging' : ''}`}
                          >
                            <div className="task-header">
                              <div className="task-title">
                                {task.status === TaskStatus.DONE && (
                                  <i className="fas fa-check task-check-icon"></i>
                                )}
                                {task.title}
                              </div>
                              <div className="task-actions">
                                <button
                                  className="task-action-button"
                                  onClick={() => handleEditTask(task)}
                                >
                                  <i className="fas fa-pencil-alt"></i>
                                </button>
                                <button
                                  className="task-action-button"
                                  onClick={() => handleDeleteTask(task.id)}
                                >
                                  <i className="fas fa-trash"></i>
                                </button>
                              </div>
                            </div>
                            <div className="task-description">{task.description}</div>
                          </div>
                        )}
                      </Draggable>
                    ))}
                    {provided.placeholder}
                  </div>
                )}
              </Droppable>
            </div>
          ))}
        </div>
      </DragDropContext>

      <TaskModal
        isOpen={isModalOpen}
        task={editingTask}
        onClose={() => setIsModalOpen(false)}
        onSave={handleSaveTask}
      />
    </div>
  );
};

export default Todo;
