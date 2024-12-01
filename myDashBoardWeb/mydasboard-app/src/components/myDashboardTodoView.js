import React, { useState, useEffect } from 'react';
import axios from "axios";
import Card from "./myDashBoardCardView";


import './../App.css';

const TodoView = () => {

    const [openCards, setOpenCards] = useState([]);
    const [inProgressCards, setInProgressCards] = useState([]);
    const [doneCards, setDoneCards] = useState([]);
    const [loading, setLoading] = useState(true);
    const [error, setError] = useState(null);

    useEffect(() => {
        const fetchOpenCards = async () => {
            try {
                const response = await axios.get("http://localhost:5020/api/todo/taskByStatus/1");
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
                const response = await axios.get("http://localhost:5020/api/todo/taskByStatus/2");
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
                const response = await axios.get("http://localhost:5020/api/todo/taskByStatus/3");
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

    if (loading) return <div>Loading...</div>;
    if (error) return <div>{error}</div>;

    return (
        <div>
            <h2>Todo</h2>
            <div class="add-new">
                <button class="add-new-button">
                    Add New Task
                </button>
            </div>
            <div class="row">
                <div class="column-a">
                    <div class="header-row">
                        <h2>Open tasks</h2>
                    </div>
                    <div class="card-list">
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
                <div class="column-b">
                    <div class="header-row">
                        <h2>Inprogress tasks</h2>
                    </div>
                    <div class="card-list">
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
                <div class="column-c">
                    <div class="header-row">
                        <h2>Done tasks</h2>
                    </div>
                    <div class="card-list">
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







