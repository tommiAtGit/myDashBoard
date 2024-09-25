import React, { useState } from 'react';

const FinanceView = () => {
    const [text1, setText1] = useState('');
    const [text2, setText2] = useState('');
    const [checkboxes, setCheckboxes] = useState({
        item1: false,
        item2: false,
        item3: false,
    });

    const handleCheckboxChange = (e) => {
        setCheckboxes({
            ...checkboxes,
            [e.target.name]: e.target.checked
        });
    };

    return (
        <div>
            <h2>Finance View</h2>
            <div>
                <label>Text Field 1:</label>
                <input type="text" value={text1} onChange={(e) => setText1(e.target.value)} />
            </div>
            <div>
                <label>Text Field 2:</label>
                <input type="text" value={text2} onChange={(e) => setText2(e.target.value)} />
            </div>
            <div>
                <h3>Checkbox Group</h3>
                <label>
                    <input
                        type="checkbox"
                        name="item1"
                        checked={checkboxes.item1}
                        onChange={handleCheckboxChange}
                    />
                    Item 1
                </label>
                <label>
                    <input
                        type="checkbox"
                        name="item2"
                        checked={checkboxes.item2}
                        onChange={handleCheckboxChange}
                    />
                    Item 2
                </label>
                <label>
                    <input
                        type="checkbox"
                        name="item3"
                        checked={checkboxes.item3}
                        onChange={handleCheckboxChange}
                    />
                    Item 3
                </label>
            </div>
        </div>
    );
};

export default FinanceView;
