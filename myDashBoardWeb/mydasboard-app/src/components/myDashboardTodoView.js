import React, { useState, useEffect } from 'react';
import axios from "axios";
import Card from "./myDashBoardCardView";
import TaskModal from "./myDashBoardTodoModal";


import './../App.css';

const TodoView = () => {

    const [openCards, setOpenCards] = useState([]);
    const [inProgressCards, setInProgressCards] = useState([]);
    const [doneCards, setDoneCards] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);
    const [isModalOpen, setModalOpen] = useState(false);

    useEffect(() => {
        const fetchOpenCards = async () => {
            try {
                const response = await axios.get("http://localhost:8080/api/todo/taskByStatus/1");
                setOpenCards(response.data);
                setLoading(false);

            }
            catch (err) {
                setError("Failed to load cards");
                setLoading(false);

            }
        };
        const fetchInProgressCards = async () => {
            try {
                const response = await axios.get("http://localhost:8080/api/todo/taskByStatus/2");
                setInProgressCards(response.data);
                setLoading(false);

            }
            catch (err) {
                setError("Failed to load cards");
                setLoading(false);

            }
        };
        const fetchDoneCards = async () => {
            try {
                const response = await axios.get("http://localhost:8080/api/todo/taskByStatus/3");
                setDoneCards(response.data);
                setLoading(false);

            }
            catch (err) {
                setError("Failed to load cards");
                setLoading(false);

            }
        };

        fetchOpenCards();
        fetchInProgressCards();
        fetchDoneCards();
    }, []);
    const handleSave = async (newTask) => {
        try {
            const response = await axios.post("http://localhost:8080/api/todo/addTask", newTask);
            const savedTask = response.data;
            if (savedTask.status === "1") {
                setOpenCards((prevCards) => [...prevCards, savedTask]);
            } else if (savedTask.status === "2") {
                setInProgressCards((prevCards) => [...prevCards, savedTask]);
            } else if (savedTask.status === "3") {
                setDoneCards((prevCards) => [...prevCards, savedTask]);
            }
            setModalOpen(false);
        } catch (error) {
            console.error("Error saving task:", error);
            setError("Failed to save task");
        }
    };
    const handleAddNewTask = () => {
        setModalOpen(true);
    }
    const handleCloseModal = () => {
        setModalOpen(false);
    }
  
    if (loading) return <div>Loading...</div>;
    if (error) return <div>{error}</div>;

    return (
        <div>
            <h2>Todo</h2>
            <div className="button-container">
                <button className="add-new-button" onClick={() => setModalOpen(true)}>
                    Add New Task
                    
                </button>
                {/* Modal Component */}
                <TaskModal
                    isOpen={isModalOpen}
                    onClose={() => setModalOpen(false)}
                    onSave={handleSave}
                />
            </div>
            <div className="row">
                <div className="column-a">
                    <div className="header-row">
                        <h2>Open tasks</h2>
                    </div>
                    <div className="card-list">
                        {openCards.map((card, index) => (
                            <Card
                                key={card.id}
                                name={card.name}
                                dateReported={card.dateReported}
                                description={card.description}
                            />
                        ))}
                    </div>
                </div>
                <div className="column-b">
                    <div className="header-row">
                        <h2>Inprogress tasks</h2>
                    </div>
                    <div className="card-list">
                        {inProgressCards.map((card, index) => (
                            <Card
                                key={card.id}
                                name={card.name}
                                dateReported={card.dateReported}
                                description={card.description}
                            />
                        ))}
                    </div>
                </div>
                <div className="column-c">
                    <div className="header-row">
                        <h2>Done tasks</h2>
                    </div>
                    <div className="card-list">
                        {doneCards.map((card, index) => (
                            <Card
                                key={card.id}
                                name={card.name}
                                dateReported={card.dateReported}
                                description={card.description}
                            />
                        ))}
                    </div>
                </div>

            </div>

        </div>
    );
};

export default TodoView;







