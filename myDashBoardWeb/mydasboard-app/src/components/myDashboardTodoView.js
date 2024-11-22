import React, { useState } from 'react';
import './../App.css';

const TodoView = () => {
    const [text1, setText1] = useState('');
    const [text2, setText2] = useState('');

    return (
        <div>
            <h2>Todo</h2>
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
                            <div class="content-row" >
                                <p>Some content</p>
                            </div>
                        </div>
                    </div>
                    <div class="card">
                        <div class="container">
                        <div class="title-container">
                            <b>Some Other Title</b>
                            </div>
                            <div class="date-container">
                                dd.mm.yyyy
                            </div>
                            <div class="clear-container"></div>
                            <div class="content-row" >
                                <p>Some other content</p>
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







