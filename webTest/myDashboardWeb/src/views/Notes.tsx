import React, { useState, useEffect } from 'react';
import { Note } from '../types';
import { loadNotes, saveNotes, formatDate, generateId } from '../utils/helpers';

interface RecallData {
  recallFromMemory: string;
  addMissingInfo: string;
  recallQuestions: string;
}

const Notes: React.FC = () => {
  const [notes, setNotes] = useState<Note[]>([]);
  const [selectedNote, setSelectedNote] = useState<Note | null>(null);
  const [activeTab, setActiveTab] = useState<'notes' | 'recall'>('notes');
  const [searchTerm, setSearchTerm] = useState('');
  const [filterBy, setFilterBy] = useState('title');
  const [isEditing, setIsEditing] = useState(false);
  const [isModalOpen, setIsModalOpen] = useState(false);
  const [newDocTitle, setNewDocTitle] = useState('');
  const [recallData, setRecallData] = useState<RecallData>({
    recallFromMemory: '',
    addMissingInfo: '',
    recallQuestions: ''
  });

  useEffect(() => {
    const loadedNotes = loadNotes();
    setNotes(loadedNotes);
    if (loadedNotes.length > 0) {
      setSelectedNote(loadedNotes[0]);
    }
  }, []);

  useEffect(() => {
    saveNotes(notes);
  }, [notes]);

  const handleNewDocument = () => {
    setNewDocTitle('');
    setIsModalOpen(true);
  };

  const handleCreateDocument = () => {
    if (!newDocTitle.trim()) return;

    const newNote: Note = {
      id: generateId(),
      title: newDocTitle,
      keywords: [],
      content: '',
      summary: '',
      createdAt: new Date(),
      updatedAt: new Date()
    };
    setNotes(prev => [newNote, ...prev]);
    setSelectedNote(newNote);
    setIsEditing(true);
    setActiveTab('notes');
    setIsModalOpen(false);
    setRecallData({
      recallFromMemory: '',
      addMissingInfo: '',
      recallQuestions: ''
    });
  };

  const handleCancelModal = () => {
    setIsModalOpen(false);
    setNewDocTitle('');
  };

  const handleEditNote = (note: Note) => {
    setSelectedNote(note);
    setIsEditing(true);
    // Load recall data if it exists in the note (stored as JSON in summary for demo)
    try {
      const noteData = JSON.parse(note.summary || '{}');
      if (noteData.recallData) {
        setRecallData(noteData.recallData);
      }
    } catch (e) {
      // If summary is not JSON, treat as regular summary
    }
  };

  const handleDeleteNote = (noteId: string) => {
    setNotes(prev => prev.filter(note => note.id !== noteId));
    if (selectedNote?.id === noteId) {
      setSelectedNote(notes[0] || null);
    }
  };

  const handleSaveNote = () => {
    if (selectedNote) {
      // Combine summary and recall data
      const summaryData = {
        summary: selectedNote.summary,
        recallData: recallData
      };
      
      setNotes(prev =>
        prev.map(note =>
          note.id === selectedNote.id
            ? { ...selectedNote, summary: JSON.stringify(summaryData), updatedAt: new Date() }
            : note
        )
      );
      setIsEditing(false);
    }
  };

  const handleDiscardChanges = () => {
    const original = notes.find(n => n.id === selectedNote?.id);
    if (original) {
      setSelectedNote(original);
      // Reset recall data
      try {
        const noteData = JSON.parse(original.summary || '{}');
        if (noteData.recallData) {
          setRecallData(noteData.recallData);
        }
      } catch (e) {
        setRecallData({
          recallFromMemory: '',
          addMissingInfo: '',
          recallQuestions: ''
        });
      }
    }
    setIsEditing(false);
  };

  const handleNoteChange = (field: keyof Note, value: any) => {
    if (selectedNote) {
      setSelectedNote({ ...selectedNote, [field]: value });
    }
  };

  const handleRecallChange = (field: keyof RecallData, value: string) => {
    setRecallData(prev => ({ ...prev, [field]: value }));
  };

  const filteredNotes = notes.filter(note => {
    const searchLower = searchTerm.toLowerCase();
    if (filterBy === 'title') {
      return note.title.toLowerCase().includes(searchLower);
    } else if (filterBy === 'keywords') {
      return note.keywords.some(keyword => keyword.toLowerCase().includes(searchLower));
    }
    return note.createdAt.toLocaleDateString().includes(searchLower);
  });

  // Parse summary to get display value
  const getDisplaySummary = (note: Note) => {
    try {
      const parsed = JSON.parse(note.summary);
      return parsed.summary || '';
    } catch (e) {
      return note.summary;
    }
  };

  return (
    <div className="notes-container">
      {/* New Document Modal */}
      {isModalOpen && (
        <div className="modal-overlay" onClick={handleCancelModal}>
          <div className="modal-content" onClick={(e) => e.stopPropagation()}>
            <h2 className="modal-header">New Document</h2>
            <form className="modal-form" onSubmit={(e) => { e.preventDefault(); handleCreateDocument(); }}>
              <div className="form-group">
                <label className="form-label">Document Title</label>
                <input
                  type="text"
                  value={newDocTitle}
                  onChange={(e) => setNewDocTitle(e.target.value)}
                  className="form-input"
                  placeholder="Enter document title"
                  required
                  autoFocus
                />
              </div>
              <div className="modal-actions">
                <button type="button" className="cancel-button" onClick={handleCancelModal}>
                  Cancel
                </button>
                <button type="submit" className="save-button">
                  Save
                </button>
              </div>
            </form>
          </div>
        </div>
      )}
      <div className="notes-header">
        <div className="notes-search">
          <input
            type="text"
            className="search-input"
            placeholder="Search..."
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
          <select
            className="filter-select"
            value={filterBy}
            onChange={(e) => setFilterBy(e.target.value)}
          >
            <option value="title">By Title</option>
            <option value="keywords">By Keywords</option>
            <option value="date">By Date</option>
          </select>
        </div>
        <button className="new-document-button" onClick={handleNewDocument}>
          New Document
        </button>
      </div>

      <div className="notes-content">
        <div className="notes-list-container">
          <div className="notes-tabs">
            <button
              className={`note-tab ${activeTab === 'notes' ? 'active' : ''}`}
              onClick={() => setActiveTab('notes')}
            >
              Notes
            </button>
            <button
              className={`note-tab ${activeTab === 'recall' ? 'active' : ''}`}
              onClick={() => setActiveTab('recall')}
            >
              Recall
            </button>
          </div>

          {activeTab === 'notes' && (
            <>
              <div className="notes-list-header">
                <span>Title</span>
                <span>Data created</span>
                <span>Key words</span>
              </div>
              <div className="notes-list">
                {filteredNotes.map(note => (
                  <div
                    key={note.id}
                    className={`note-item ${selectedNote?.id === note.id ? 'selected' : ''}`}
                    onClick={() => {
                      setSelectedNote(note);
                      setIsEditing(false);
                    }}
                  >
                    <div className="note-info">
                      <div className="note-item-title">{note.title}</div>
                      <div className="note-item-meta">
                        {formatDate(note.createdAt)} • {note.keywords.join(', ')}
                      </div>
                    </div>
                    <div className="note-item-actions">
                      <button
                        className="task-action-button"
                        onClick={(e) => {
                          e.stopPropagation();
                          handleEditNote(note);
                        }}
                      >
                        <i className="fas fa-pencil-alt"></i>
                      </button>
                      <button
                        className="task-action-button"
                        onClick={(e) => {
                          e.stopPropagation();
                          handleDeleteNote(note.id);
                        }}
                      >
                        <i className="fas fa-trash"></i>
                      </button>
                    </div>
                  </div>
                ))}
              </div>
            </>
          )}
        </div>

        <div className="note-detail-container">
          {selectedNote ? (
            <>
              <div className="note-detail-header">
                {isEditing ? (
                  <input
                    type="text"
                    className="form-input"
                    value={selectedNote.title}
                    onChange={(e) => handleNoteChange('title', e.target.value)}
                  />
                ) : (
                  <div className="note-detail-title">{selectedNote.title}</div>
                )}
                <div className="note-detail-date">{formatDate(selectedNote.createdAt)}</div>
              </div>

              {activeTab === 'notes' ? (
                <div className="note-sections">
                  <div className="note-section">
                    <div className="note-section-title">Key words</div>
                    {isEditing ? (
                      <input
                        type="text"
                        className="form-input"
                        value={selectedNote.keywords.join(', ')}
                        onChange={(e) =>
                          handleNoteChange('keywords', e.target.value.split(',').map(k => k.trim()))
                        }
                        placeholder="Enter keywords separated by commas"
                      />
                    ) : (
                      <div className="note-section-content">
                        {selectedNote.keywords.join(', ') || 'No keywords'}
                      </div>
                    )}
                  </div>

                  <div className="note-section">
                    <div className="note-section-title">Notes</div>
                    {isEditing ? (
                      <textarea
                        className="form-textarea note-section-content editable"
                        value={selectedNote.content}
                        onChange={(e) => handleNoteChange('content', e.target.value)}
                        placeholder="Enter your notes here..."
                      />
                    ) : (
                      <div className="note-section-content">
                        {selectedNote.content || 'No content'}
                      </div>
                    )}
                  </div>

                  <div className="note-section">
                    <div className="note-section-title">Summary</div>
                    {isEditing ? (
                      <textarea
                        className="form-textarea note-section-content editable"
                        value={(() => {
                          try {
                            const parsed = JSON.parse(selectedNote.summary);
                            return parsed.summary || '';
                          } catch (e) {
                            return selectedNote.summary;
                          }
                        })()}
                        onChange={(e) => handleNoteChange('summary', e.target.value)}
                        placeholder="Enter summary..."
                      />
                    ) : (
                      <div className="note-section-content">
                        {getDisplaySummary(selectedNote) || 'No summary'}
                      </div>
                    )}
                  </div>
                </div>
              ) : (
                <div className="note-sections">
                  <div className="note-section">
                    <div className="note-section-title">Recall from memory</div>
                    {isEditing ? (
                      <textarea
                        className="form-textarea note-section-content editable"
                        value={recallData.recallFromMemory}
                        onChange={(e) => handleRecallChange('recallFromMemory', e.target.value)}
                        placeholder="Enter recall from memory..."
                        style={{ minHeight: '100px' }}
                      />
                    ) : (
                      <div className="note-section-content">
                        {recallData.recallFromMemory || 'No recall data'}
                      </div>
                    )}
                  </div>

                  <div className="recall-buttons">
                    <div className="note-section" style={{ flex: 1 }}>
                      <div className="note-section-title">Add missing information</div>
                      {isEditing ? (
                        <textarea
                          className="form-textarea note-section-content editable"
                          value={recallData.addMissingInfo}
                          onChange={(e) => handleRecallChange('addMissingInfo', e.target.value)}
                          placeholder="Enter missing information..."
                          style={{ minHeight: '150px' }}
                        />
                      ) : (
                        <div className="note-section-content" style={{ minHeight: '150px' }}>
                          {recallData.addMissingInfo || 'No missing information'}
                        </div>
                      )}
                    </div>
                    
                    <div className="note-section" style={{ flex: 1 }}>
                      <div className="note-section-title">Recall questions</div>
                      {isEditing ? (
                        <textarea
                          className="form-textarea note-section-content editable"
                          value={recallData.recallQuestions}
                          onChange={(e) => handleRecallChange('recallQuestions', e.target.value)}
                          placeholder="Enter recall questions..."
                          style={{ minHeight: '150px' }}
                        />
                      ) : (
                        <div className="note-section-content" style={{ minHeight: '150px' }}>
                          {recallData.recallQuestions || 'No recall questions'}
                        </div>
                      )}
                    </div>
                  </div>
                </div>
              )}

              {isEditing && (
                <div className="note-actions">
                  <button className="discard-button" onClick={handleDiscardChanges}>
                    Discard changes
                  </button>
                  <button className="save-note-button" onClick={handleSaveNote}>
                    Save
                  </button>
                </div>
              )}
            </>
          ) : (
            <div className="empty-state">
              <div className="empty-state-icon">
                <i className="fas fa-sticky-note"></i>
              </div>
              <div className="empty-state-text">No note selected</div>
              <div className="empty-state-subtext">Select a note from the list or create a new one</div>
            </div>
          )}
        </div>
      </div>
    </div>
  );
};

export default Notes;
