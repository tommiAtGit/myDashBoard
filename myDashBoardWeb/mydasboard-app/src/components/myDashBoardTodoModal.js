import React, { useState } from "react";
import "./../todoModal.css";

const AddNewTaskModal = ({ isOpen, onClose, onSave }) => {

    const [taskName, setTaskName] = useState("");
    const [taskDescription, setTaskDescription] = useState("");
    const [taskDateReported, setTaskDateReported] = useState("");
    const [taskStatus, setTaskStatus] = useState("1"); // Default to "Open" status
    const [taskAssignedTo, setTaskAssignedTo] = useState("");
    const [taskCreatedBy, setTaskCreatedBy] = useState("");
    const [taskCreatedDate, setTaskCreatedDate] = useState("");
    const [taskDueDate, setTaskDueDate] = useState("");
    const [taskLastUpdated, setTaskLastUpdated] = useState("");
    const [taskLastUpdatedBy, setTaskLastUpdatedBy] = useState("");

    const handleSave = () => {
        const newTask = {
            name: taskName,
            description: taskDescription,
            dateReported: taskDateReported,
            status: taskStatus,
            assignedTo: taskAssignedTo,
            createdBy: taskCreatedBy,
            createdDate: taskCreatedDate,
            dueDate: taskDueDate,
            lastUpdated: taskLastUpdated,
            lastUpdatedBy: taskLastUpdatedBy
        };

        onSave(newTask);
        onClose();
    }
    const handleCancel = () => {
        onClose();
    }
    if (!isOpen) return null;   
    return (
        <div className="modal-overlay">
          <div className="modal-container">
            <h2 className="modal-title">Add New Task</h2>
            
            {/* Title Input */}
            <input
              type="text"
              placeholder="Enter task title"
              value={taskName}
              onChange={(e) => setTaskName(e.target.value)}
              className="modal-input"
            />
            
            {/* Description Input */}
            <input
              type="text"
              placeholder="Enter task description"
              value={taskDescription}
              onChange={(e) => setTaskDescription(e.target.value)}
              className="modal-input"
            />
            
            {/* Buttons */}
            <div className="modal-buttons">
              <button onClick={onClose} className="modal-cancel">Cancel</button>
              <button onClick={() => onSave({ taskName, taskDescription })} className="modal-save">Save</button>
            </div>
          </div>
        </div>
      );
    };
    
export default AddNewTaskModal;
