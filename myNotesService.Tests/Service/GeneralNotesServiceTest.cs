using myNotesService.Model;
using myNotesService.Domain;
using myNotesService.Mapper;
using myNotesService.Repository;
using myNotesService.Services;
using AutoMapper;
using myNotesService.TestUtils;

namespace myNotesService.Tests
{

    public class GeneneralNotesServiceTest
    {

        private GeneralNotesService _service;
        private IMapper _mapper;

        public GeneneralNotesServiceTest()
        {
            var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>(); // Reuse your main profile
        });

            _mapper = config.CreateMapper();
            _service = new(_mapper);

        }

        [Fact]
        public void AddNewGenralNotesTest()
        {
            // Given
            var notes = NotesTestUtils.CreateGeneralNotes();
            // When
            var result = _service.AddGeneralNotes(notes);
            var newNotes = _service.GetAllGeneralNotes();

            // Then
            Assert.NotNull(newNotes);
            Assert.Single(newNotes);
            var n = newNotes.FirstOrDefault();
            Assert.NotNull(n);
            Assert.Equal(notes.DateCreatad, n.DateCreatad);
            Assert.Equal(notes.DateModifyed, n.DateModifyed);
            Assert.Equal(notes.ModifiedBy, n.ModifiedBy);

        }

        [Fact]
        public void GetGeneralNotesById()
        {
            // Given
            var notes = NotesTestUtils.CreateGeneralNotes();
            var newNotes = _service.AddGeneralNotes(notes);
            Assert.NotNull(newNotes);
            var id = newNotes.id;

            // When
            var result = _service.GetGeneralNotesById(id);
            // Then
            Assert.NotNull(result);
            Assert.Equal(newNotes.id, result.id);

        }
        [Fact]
        public void GetGeneralNotesById_NotFound()
        {
            //When
            var notes = NotesTestUtils.CreateGeneralNotes();
            var newNotes = _service.AddGeneralNotes(notes);
            Assert.NotNull(newNotes);
            var id = Guid.NewGuid();

            // When
            var result = _service.GetGeneralNotesById(id);

            // Then
            Assert.Equal(result.id, Guid.Empty);
        }
        [Fact]
        public void GetGeneralNotesByDateCreated()
        {
            // Given
            var notes = NotesTestUtils.CreateMultipleGeneralNotes();
            foreach (GeneralNotes n in notes)
            {
                var newNotes = _service.AddGeneralNotes(n);
                Assert.NotNull(newNotes);
            }

            // When
            var result = _service.GetGeneralNotesByDateCreated(DateTime.Now.AddDays(-3));
            // Then
            Assert.NotNull(result);
            Assert.True(result.Any());
            Assert.Equal(DateTime.Now.AddDays(-3).Date, result.First().DateCreatad.Date);
        }
        [Fact]
        public void GetGeneralNotesByDateCreated_NotFound()
        {
            // Given
            var notes = NotesTestUtils.CreateMultipleGeneralNotes();
            foreach (GeneralNotes n in notes)
            {
                var newNotes = _service.AddGeneralNotes(n);
                Assert.NotNull(newNotes);
            }

            // When
            var result = _service.GetGeneralNotesByDateCreated(DateTime.Now.AddDays(-20));
            // Then
            Assert.NotNull(result);
            Assert.False(result.Any());

        }

        [Fact]
        public void GetGeneralNotesByOwner()
        {
            // Given
            var notes = NotesTestUtils.CreateMultipleGeneralNotes();
            foreach (GeneralNotes n in notes)
            {
                var newNotes = _service.AddGeneralNotes(n);
                Assert.NotNull(newNotes);
            }

            // When
            var result = _service.GetGeneralNotesByOwner("Owner Tommi_#2");
            // Then
            Assert.NotNull(result);
            Assert.Single(result);

        }
        [Fact]
        public void GetGeneralNotesByOwner_NotFound()
        {
            // Given
            var notes = NotesTestUtils.CreateMultipleGeneralNotes();
            foreach (GeneralNotes n in notes)
            {
                var newNotes = _service.AddGeneralNotes(n);
                Assert.NotNull(newNotes);
            }

            // When
            var result = _service.GetGeneralNotesByOwner("Tommi_#2");
            // Then
            Assert.NotNull(result);
            Assert.True(result.Count() < 1);
        }

        [Fact]
        public void GetGeneralNotesByKeyWords()
        {
            // Given
            var notes = NotesTestUtils.CreateMultipleGeneralNotes();
            foreach (GeneralNotes n in notes)
            {
                var newNotes = _service.AddGeneralNotes(n);
                Assert.NotNull(newNotes);
            }
            List<string> keyWords = [
                "Test_#1.2",
                "Test_#2.2",
                "Test_#4.2",

            ];
            // When
            var result = _service.GetGeneralNotesByKeyWords(keyWords);

            // Then
            Assert.NotNull(result);
            Assert.Equal(3, result.Count());
        }

        [Fact]
        public void GetGeneralNotesByKeyWords_NotFound()
        {
            // Given
            var notes = NotesTestUtils.CreateMultipleGeneralNotes();
            foreach (GeneralNotes n in notes)
            {
                var newNotes = _service.AddGeneralNotes(n);
                Assert.NotNull(newNotes);
            }
            List<string> keyWords = [
                "Test_# 1",
                "Test_# 2",
                "Test_# 4",

            ];
            // When
            var result = _service.GetGeneralNotesByKeyWords(keyWords);

            // Then
            Assert.NotNull(result);
            Assert.True(result.Count() < 1);
        }

        [Fact]
        public void UpdateGeneralNotes()
        {
            var notes = NotesTestUtils.CreateMultipleGeneralNotes();
            foreach (GeneralNotes n in notes)
            {
                var newNotes = _service.AddGeneralNotes(n);
                Assert.NotNull(newNotes);
            }
            var theNotes = _service.GetAllGeneralNotes();

            // When
            Assert.True(theNotes.Any());
            GeneralNotes updNotes = theNotes.First();
            Guid id = theNotes.First().id;
            updNotes.Notes = "Update test";
            updNotes.NotesConclution = "Update conclution";
            updNotes.NotesTiltle = "Update test";
            _service.UpdateGeneralNotes(id, updNotes);
            // Then
            GeneralNotes updResult = _service.GetGeneralNotesById(id);
            Assert.NotNull(updResult);
            Assert.Equal(updNotes.Notes, updResult.Notes);
            Assert.Equal(updNotes.NotesConclution, updResult.NotesConclution);
            Assert.Equal(updNotes.NotesTiltle, updResult.NotesTiltle);

        }
        [Fact]
        public void DeleteGeneralNotes()
        {
            var notes = NotesTestUtils.CreateMultipleGeneralNotes();
            foreach (GeneralNotes n in notes)
            {
                var newNotes = _service.AddGeneralNotes(n);
                Assert.NotNull(newNotes);
            }
            var theNotes = _service.GetAllGeneralNotes();


            // When
            var result = _service.DeleteGeneralNote(theNotes.First().id);

            // Then
            Assert.True(result);
        }

        [Fact]
        public void DeleteGeneralNotes_NotFound()
        {
            // Given
            var notes = NotesTestUtils.CreateMultipleGeneralNotes();
            foreach (GeneralNotes n in notes)
            {
                var newNotes = _service.AddGeneralNotes(n);
                Assert.NotNull(newNotes);
            }
            var theNotes = _service.GetAllGeneralNotes();


            // When
            var result = _service.DeleteGeneralNote(Guid.NewGuid());

            // Then
            Assert.False(result);


        }

    }
}