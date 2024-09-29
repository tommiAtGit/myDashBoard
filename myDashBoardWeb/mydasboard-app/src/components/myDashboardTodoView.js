import React, { useState } from 'react';

const TodoView = () => {
    const [text1, setText1] = useState('');
    const [text2, setText2] = useState('');

    return (
        <div>
            <h2>Todo</h2>
            <div>
                <label>Text Field 1:</label>
                <input type="text" value={text1} onChange={(e) => setText1(e.target.value)} />
            </div>
            <div>
                <label>Text Field 2:</label>
                <input type="text" value={text2} onChange={(e) => setText2(e.target.value)} />
            </div>
        </div>
    );
};

export default TodoView;
