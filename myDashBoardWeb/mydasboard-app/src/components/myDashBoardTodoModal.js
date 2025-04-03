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
                {/* Date Reported Input */}
                <label htmlFor="date-reported">Date Reported
                    <input
                        type="date"
                        placeholder="Enter date reported"
                        value={taskDateReported}
                        onChange={(e) => setTaskDateReported(e.target.value)}
                        className="modal-input"
                    />
                </label>

                {/* Status Input */}
                <select
                    value={taskStatus}
                    onChange={(e) => setTaskStatus(e.target.value)}
                    className="modal-input"
                >
                    <option value="1">Open</option>
                    <option value="2">In Progress</option>
                    <option value="3">Done</option>
                </select>
                {/* Assigned To Input */}
                <input
                    type="text"
                    placeholder="Assigned To"
                    value={taskAssignedTo}
                    onChange={(e) => setTaskAssignedTo(e.target.value)}
                    className="modal-input"
                />
                {/* Created By Input */}
                <input
                    type="text"
                    placeholder="Created By"
                    value={taskCreatedBy}
                    onChange={(e) => setTaskCreatedBy(e.target.value)}
                    className="modal-input"/>

                    

                {/* Buttons */}
                <div className="modal-buttons">
                    <button onClick={() => handleCancel()} className="modal-cancel">
                        Cancel
                    </button>
                    <button onClick={() => handleSave()} className="modal-save">
                        Save
                    </button>
                </div>
            </div>
        </div>
    );
};

export default AddNewTaskModal;
