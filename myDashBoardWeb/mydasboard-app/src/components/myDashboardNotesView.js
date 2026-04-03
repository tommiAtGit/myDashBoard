import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { FaPen, FaTrash } from 'react-icons/fa';
import './../App.css';

const NotesView = () => {
    const [activeTab, setActiveTab] = useState('notes');
    const [notes, setNotes] = useState([]);
    const [selectedNote, setSelectedNote] = useState(null);
    const [searchTitle, setSearchTitle] = useState('');
    const [searchBy, setSearchBy] = useState('');
    const [loading, setLoading] = useState(false);
    const [error, setError] = useState(null);

    // Recall exercise local state
    const [recallFromMemory, setRecallFromMemory] = useState('');
    const [missingInformation, setMissingInformation] = useState('');
    const [recallQuestions, setRecallQuestions] = useState('');

    const baseUrl = "http://localhost:5003/api/GeneneralNotes";

    useEffect(() => {
        fetchNotes();
    }, []);

    const fetchNotes = async () => {
        setLoading(true);
        try {
            const response = await axios.get(baseUrl);
            setNotes(response.data);
            if (response.data.length > 0 && !selectedNote) {
                setSelectedNote(response.data[0]);
            }
        } catch (err) {
            console.error("Error fetching notes:", err);
            setError("Failed to load notes");
        } finally {
            setLoading(false);
        }
    };

    const handleSave = async () => {
        if (!selectedNote) return;
        try {
            const response = await axios.put(`${baseUrl}/${selectedNote.id}`, selectedNote);
            const updatedNote = response.data;
            setNotes(notes.map(n => n.id === updatedNote.id ? updatedNote : n));
            alert("Changes saved successfully");
        } catch (err) {
            console.error("Error saving note:", err);
            alert("Failed to save changes");
        }
    };

    const handleDelete = async (id, e) => {
        if (e) e.stopPropagation();
        if (!window.confirm("Are you sure you want to delete this note?")) return;
        
        try {
            await axios.delete(`${baseUrl}/${id}`);
            const updatedNotes = notes.filter(n => n.id !== id);
            setNotes(updatedNotes);
            if (selectedNote && selectedNote.id === id) {
                setSelectedNote(updatedNotes.length > 0 ? updatedNotes[0] : null);
            }
        } catch (err) {
            console.error("Error deleting note:", err);
            alert("Failed to delete note");
        }
    };

    const handleNewDocument = () => {
        const newNote = {
            id: crypto.randomUUID(),
            notesTiltle: "New Document",
            notes: "",
            notesConclution: "",
            keyWords: [],
            dateCreatad: new Date().toISOString(),
            owner: "Current User"
        };
        setSelectedNote(newNote);
        setNotes([newNote, ...notes]);
    };

    const handleDiscard = () => {
        if (window.confirm("Discard all unsaved changes?")) {
            setRecallFromMemory('');
            setMissingInformation('');
            setRecallQuestions('');
        }
    };

    const formatDate = (dateString) => {
        if (!dateString) return "DD.MM.YYY";
        const date = new Date(dateString);
        const day = String(date.getDate()).padStart(2, '0');
        const month = String(date.getMonth() + 1).padStart(2, '0');
        const year = date.getFullYear();
        return `${day}.${month}.${year}`;
    };

    const handleInputChange = (e) => {
        const { name, value } = e.target;
        setSelectedNote({
            ...selectedNote,
            [name]: value
        });
    };

    // Filter notes for sidebar
    const filteredNotes = notes.filter(note => {
        const titleMatch = (note.notesTiltle || "").toLowerCase().includes(searchTitle.toLowerCase());
        const byMatch = (note.owner || "").toLowerCase().includes(searchBy.toLowerCase());
        return titleMatch && byMatch;
    });

    return (
        <div className="notes-view-container">
            <div className="notes-upper-layout">
                {/* Sidebar */}
                <div className="notes-sidebar-container">
                    <div className="sidebar-search-row">
                        <div className="sidebar-search-item">
                            <label>Search</label>
                            <input 
                                type="text" 
                                className="sidebar-input" 
                                value={searchTitle}
                                onChange={(e) => setSearchTitle(e.target.value)}
                            />
                        </div>
                        <div className="sidebar-search-item">
                            <label>By</label>
                            <input 
                                type="text" 
                                className="sidebar-input" 
                                value={searchBy}
                                onChange={(e) => setSearchBy(e.target.value)}
                            />
                        </div>
                    </div>

                    <div className="sidebar-list-header">
                        <div className="col-title">Title</div>
                        <div className="col-date">Data crated</div>
                        <div className="col-keys">Key words</div>
                    </div>

                    <div className="notes-list-scroll">
                        {filteredNotes.map(note => (
                            <div 
                                key={note.id} 
                                className={`note-item-row ${selectedNote?.id === note.id ? 'active' : ''}`}
                                onClick={() => setSelectedNote(note)}
                            >
                                <div className="col-title" style={{ overflow: 'hidden', whiteSpace: 'nowrap', textOverflow: 'ellipsis' }}>
                                    {note.notesTiltle}
                                </div>
                                <div className="col-date">{formatDate(note.dateCreatad)}</div>
                                <div className="col-keys" style={{ overflow: 'hidden', whiteSpace: 'nowrap', textOverflow: 'ellipsis' }}>
                                    {note.keyWords?.join(', ')}
                                </div>
                                <div className="note-row-actions">
                                    <FaPen onClick={() => setSelectedNote(note)} />
                                    <FaTrash onClick={(e) => handleDelete(note.id, e)} />
                                </div>
                            </div>
                        ))}
                    </div>
                </div>

                {/* Content Area */}
                <div className="notes-content-area">
                    <div className="new-doc-btn-container">
                        <div style={{ width: '30%', visibility: 'hidden' }}></div>
                        <button className="btn-new-document" onClick={handleNewDocument}>New Document</button>
                    </div>

                    {error && <div className="error-box">{error}</div>}

                    <div className="tabs-navigation">
                        <div 
                            className={`tab-item ${activeTab === 'notes' ? 'active' : ''}`}
                            onClick={() => setActiveTab('notes')}
                        >
                            Notes
                        </div>
                        <div 
                            className={`tab-item ${activeTab === 'recall' ? 'active' : ''}`}
                            onClick={() => setActiveTab('recall')}
                        >
                            Recall
                        </div>
                    </div>

                    <div className="panel-container">
                        <div className="panel-title-bar">
                            <div>{selectedNote?.notesTiltle || "Note Title"}</div>
                            <div>{formatDate(selectedNote?.dateCreatad)}</div>
                        </div>

                        <div className="panel-content-body">
                            {activeTab === 'notes' ? (
                                <div className="notes-layout-grid">
                                    <div className="notes-section-keywords">
                                        <label className="area-header">Key words</label>
                                        <textarea 
                                            className="area-textarea"
                                            value={selectedNote?.keyWords?.join(', ') || ''}
                                            onChange={(e) => {
                                                const keys = e.target.value.split(',').map(k => k.trim());
                                                setSelectedNote({...selectedNote, keyWords: keys});
                                            }}
                                        />
                                    </div>
                                    <div className="notes-section-main">
                                        <label className="area-header">Notes</label>
                                        <textarea 
                                            name="notes"
                                            className="area-textarea"
                                            value={selectedNote?.notes || ''}
                                            onChange={handleInputChange}
                                        />
                                    </div>
                                    <div className="notes-section-summary">
                                        <label className="area-header">Summary</label>
                                        <textarea 
                                            name="notesConclution"
                                            className="area-textarea"
                                            value={selectedNote?.notesConclution || ''}
                                            onChange={handleInputChange}
                                        />
                                    </div>
                                </div>
                            ) : (
                                <div className="recall-layout-stack">
                                    <div className="recall-section-memory">
                                        <label className="area-header">Recall from memory</label>
                                        <textarea 
                                            className="area-textarea"
                                            value={recallFromMemory}
                                            onChange={(e) => setRecallFromMemory(e.target.value)}
                                        />
                                    </div>
                                    <div className="recall-section-row">
                                        <div className="recall-section-missing">
                                            <label className="area-header">Add missing information</label>
                                            <textarea 
                                                className="area-textarea"
                                                value={missingInformation}
                                                onChange={(e) => setMissingInformation(e.target.value)}
                                            />
                                        </div>
                                        <div className="recall-section-questions">
                                            <label className="area-header">Recall questions</label>
                                            <textarea 
                                                className="area-textarea"
                                                value={recallQuestions}
                                                onChange={(e) => setRecallQuestions(e.target.value)}
                                            />
                                        </div>
                                    </div>
                                </div>
                            )}
                        </div>
                    </div>

                    <div className="view-footer-actions">
                        <button className="btn-discard-changes" onClick={handleDiscard}>Discard changes</button>
                        <button className="btn-save-note" onClick={handleSave}>Save</button>
                    </div>
                </div>
            </div>

            {loading && <div className="loading-spinner">Loading...</div>}
        </div>
    );
};

export default NotesView;
