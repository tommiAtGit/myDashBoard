import React, { useState, useEffect } from "react";
import "./../todoModal.css";

const AddNewTaskModal = ({ isOpen, onClose, onSave, task }) => {

    const [taskId, setTaskId] = useState(crypto.randomUUID());
    const [taskName, setTaskName] = useState("");
    const [taskDescription, setTaskDescription] = useState("");
    const [taskDateReported, setTaskDateReported] = useState("");
    const [taskStatus, setTaskStatus] = useState("1"); // Default to "Open" status
    const [taskAssignedTo, setTaskAssignedTo] = useState("");
    const [taskCreatedBy, setTaskCreatedBy] = useState("");

    useEffect(() => {
        if (task && isOpen) {
            setTaskId(task.id || crypto.randomUUID());
            setTaskName(task.name || "");
            setTaskDescription(task.description || "");
            setTaskDateReported(task.dateReported ? task.dateReported.split('T')[0] : "");
            setTaskStatus(task.status ? task.status.toString() : "1");
            setTaskAssignedTo(task.owner || "");
            setTaskCreatedBy(task.reporter || "");
        } else if (isOpen) {
            // Reset fields for a new task
            setTaskId(crypto.randomUUID());
            setTaskName("");
            setTaskDescription("");
            setTaskDateReported("");
            setTaskStatus("1");
            setTaskAssignedTo("");
            setTaskCreatedBy("");
        }
    }, [task, isOpen]);
    

    const handleSave = () => {
        const newTask = {
            id: taskId,
            name: taskName,
            description: taskDescription,
            reporter: taskCreatedBy,
            owner: taskAssignedTo,

            //status: taskStatus,
            status:Number(taskStatus),
            dateReported: handleNewDate(),

            dateOpened: handleNewDate(),
            dataCompleted: handleNewDate(),
            dateClosed: handleNewDate(),
        };

        onSave(newTask);
        onClose();
    }

    const handleUpdate=() => {
        const updatedTask = {
            id: taskId,
            name: taskName,
        }
    }
    const handleNewDate = (e) => {
        console.log("At handleNewDate");
        const newDate = new Date().toISOString();
        console.log("New date:", newDate);
        return newDate;
    }

    const handleCancel = () => {
        onClose();
    }
    if (!isOpen) return null;
    return (
        <div className="modal-overlay">
            <div className="modal-container">
                <h2 className="modal-title">Task</h2>

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
                    <option value="5">Closed</option>
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
                    className="modal-input" />



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
