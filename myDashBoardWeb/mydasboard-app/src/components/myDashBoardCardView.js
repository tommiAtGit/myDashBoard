import React from "react";
import axios from "axios";
import { FaPen, FaTrash } from "react-icons/fa";
import './../App.css';

 //const baseUrl = "http://localhost:8080/api/todo";
 const baseUrl = "http://localhost:5001/api/todo";

const formatDate = (isoDateString) => {
    const date = new Date(isoDateString); // Parse the ISO date string
    const day = String(date.getDate()).padStart(2, '0'); // Add leading zero
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Months are 0-indexed
    const year = date.getFullYear();

    return `${day}.${month}.${year}`; // Return in dd.mm.yyyy format
};

const Card = ({ id, name, dateReported , description, onEdit, onDelete }) => {
    const handleEdit = async (e) => {
        console.log("Edit button clicked for card ID:", id);
        try {
            const response = await axios.get(baseUrl + "/" + id);
            if (response.status === 200 && response.data) {
                const task = response.data;
                // Validate the object
                if (task && task.id && task.name) {
                    onEdit(task); // Pass the validated object to the modal through the onEdit callback
                } else {
                    console.error("Task validation failed:", task);
                }
            } else {
                console.error("Failed to fetch task from:", baseUrl + "/" + id);
            }
        } catch (error) {
            console.error("Error calling GetTaskById:", error);
        }
    };
    const handleDelete = async () => {
        console.log("Delete button clicked for card ID:", id);
        try {
            const response = await axios.delete(`${baseUrl}/delete/${id}`);
            if (response.status === 204) {
                console.log("Card deleted successfully");
                if (onDelete) {
                    onDelete(id);
                }
            } else {
                console.error("Failed to delete card");
            }
        } catch (error) {
            console.error("Error deleting card:", error);
        }
    };

    return (
        <div className="card">
            <div className="container">
                <div className="title-container">
                    <b>{name}</b>
                </div>
                <div className="date-container">{formatDate(dateReported)}</div>
                <div className="clear-container"></div>
                <div className="split-content-container">
                    <div className="content-container">{description}</div>
                    <div className="action-container">
                        <FaPen onClick={handleEdit} />
                        <FaTrash onClick={handleDelete}/>
                    </div>
                </div>
            </div>
        </div>
    );
    
};

export default Card;