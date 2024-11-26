import React, { useState } from 'react';

import { FaPen } from "react-icons/fa";
import { FaTrash } from "react-icons/fa";

import './../App.css';

const TodoView = () => {

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
                        <h2>Column 1</h2>
                    </div>
                    <div class="card">
                        <div class="container">
                            <div class="title-container">
                                <b>Some Title</b>
                            </div>
                            <div class="date-container">
                                dd.mm.yyyy
                            </div>
                            <div class="clear-container"></div>
                            <div class="split-content-container">
                            <div class="content-container">
                            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi malesuada at lacus et tincidunt. Nunc facilisis laoreet odio ut vestibulum.
                            </div>
                            <div class="action-container">
                                <FaPen/>
                                <FaTrash/>
                            </div>
                            </div>
                        </div>
                    </div>

                    <div class="card">
                        <div class="container">
                            <div class="title-container">
                                <b>Some Title</b>
                            </div>
                            <div class="date-container">
                                dd.mm.yyyy
                            </div>
                            <div class="clear-container"></div>
                            <div class="split-content-container">
                            <div class="content-container">
                            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi malesuada at lacus et tincidunt. Nunc facilisis laoreet odio ut vestibulum.
                            </div>
                            <div class="action-container">
                                <FaPen/>
                                <FaTrash/>
                            </div>
                            </div>
                        </div>
                    </div>


                </div>
                <div class="column-b">
                    <h2>Column 2</h2>
                    <p>Some text..</p>
                </div>
                <div class="column-c">
                    <h2>Column 3</h2>
                    <p>Some text..</p>
                </div>

            </div>

        </div>
    );
};

export default TodoView;







