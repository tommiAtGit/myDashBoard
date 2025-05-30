import React from 'react';
import { Link } from 'react-router-dom';
import { IoHome } from "react-icons/io5";
import { FaDollarSign } from "react-icons/fa";
import { FaTools } from "react-icons/fa";
import { FaPencilAlt } from "react-icons/fa";


const SideMenu = () => {
    return (
        <div className="side-menu">
            <ul>
                <li><IoHome /><Link to="/DashboardView">myDasboard</Link></li>
                <li><FaDollarSign /><Link to="/FinanceView">my Finance</Link></li>
                <li><FaTools /><Link to="/TodoView">my Todo</Link></li>
                <li><FaPencilAlt /><Link to="/NotesView">my Notes</Link> </li>
            </ul>
        </div>
    );
};

export default SideMenu;
