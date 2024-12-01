import React from "react";
import { FaPen, FaTrash } from "react-icons/fa";
import './../App.css';

const formatDate = (isoDateString) => {
    const date = new Date(isoDateString); // Parse the ISO date string
    const day = String(date.getDate()).padStart(2, '0'); // Add leading zero
    const month = String(date.getMonth() + 1).padStart(2, '0'); // Months are 0-indexed
    const year = date.getFullYear();

    return `${day}.${month}.${year}`; // Return in dd.mm.yyyy format
};

const Card = ({ id, name, dateReported , description }) => {
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
                        <FaPen />
                        <FaTrash />
                    </div>
                </div>
            </div>
        </div>
    );
};

export default Card;