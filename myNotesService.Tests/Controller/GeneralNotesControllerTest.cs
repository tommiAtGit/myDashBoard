using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using myNotesService.Controllers;
using myNotesService.Model;
using myNotesService.Services;
using myNotesService.TestUtils;
using Microsoft.AspNetCore.Http.HttpResults;

namespace myTodoService.Tests.Controllers
{
    public class GeneralNotesControllerTests
    {

        private readonly Mock<IGeneralNotesService> _mockService;
        private readonly GeneneralNotesController _controller;
        private readonly NotesTestUtils _testUtils;

        public GeneralNotesControllerTests()
        {
            // Arrange
            _mockService = new Mock<IGeneralNotesService>();
            _controller = new GeneneralNotesController(_mockService.Object);
            _testUtils = new();
        }
        [Fact]
        public void GetAllGeneralNotes_returnsOk_andObject()
        {
            // Given
            IEnumerable<GeneralNotes> notes = NotesTestUtils.CreateMultipleGeneralNotes();
            _mockService.Setup(service => service.GetAllGeneralNotes()).Returns(notes);
            // When
            var result = _controller.GetAllGeneralNotes();
            // Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedTasks = Assert.IsAssignableFrom<IEnumerable<GeneralNotes>>(okResult.Value);
            Assert.Equal(_testUtils.GetNumberOfNotes(), returnedTasks?.Count());

        }
        [Fact]
        public void GetAllGeneralNotes_retrunsNotFound()
        {
            IEnumerable<GeneralNotes> notes = [];
            // Given
            _mockService.Setup(service => service.GetAllGeneralNotes()).Returns(notes);
            // When
            var result = _controller.GetAllGeneralNotes();

            // Then
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);

        }

        [Fact]
        public void GetGeneralNotesById_returnOK_andObject()
        {
            // Given
            GeneralNotes notes = NotesTestUtils.CreateGeneralNotes();
            Guid id = notes.id;
            _mockService.Setup(service => service.GetGeneralNotesById(id)).Returns(notes);
            // When
            var result = _controller.GetGeneralNotesById(id);
            // Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedNotes = Assert.IsAssignableFrom<GeneralNotes>(okResult.Value);
            Assert.Equal(id, returnedNotes.id);
            Assert.Equal(notes.DateCreatad, returnedNotes.DateCreatad);
            Assert.Equal(notes.ModifiedBy, returnedNotes.ModifiedBy);
            Assert.Equal(notes.Notes, returnedNotes.Notes);
            Assert.Equal(notes.NotesConclution, returnedNotes.NotesConclution);



        }
        [Fact]
        public void GetGeneralNotesById_returnNotFound_IdEmpty()
        {
            // Given
            GeneralNotes notes = NotesTestUtils.CreateGeneralNotes();
            Guid id = Guid.Empty;
            _mockService.Setup(service => service.GetGeneralNotesById(id)).Returns(notes);
            // When
            var result = _controller.GetGeneralNotesById(id);
            // Then
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);


        }
        [Fact]
        public void GetGeneralNotesById_returnNotFound_ObjectNotFound()
        {
            // Given
            GeneralNotes notes = NotesTestUtils.CreateGeneralNotes();
            Guid id = notes.id;
            _mockService.Setup(service => service.GetGeneralNotesById(id)).Returns(notes);
            // When
            var result = _controller.GetGeneralNotesById(Guid.NewGuid());
            // Then
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);


        }
        [Fact]
        public void GetNotesByKeyword_returnOK_and_Object()
        {
            // Given
            List<GeneralNotes> notes = NotesTestUtils.CreateMultipleGeneralNotes();
            List<string> keyWords = ["Testing", "Testing", "Notes", "DashBoard"];

            _mockService.Setup(service => service.GetGeneralNotesByKeyWords(keyWords)).Returns(notes);
            // When
            var result = _controller.GetNotesByKeyWord(keyWords);
            // Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedNotes = Assert.IsAssignableFrom<IEnumerable<GeneralNotes>>(okResult.Value);
            Assert.Equal(_testUtils.GetNumberOfNotes(), returnedNotes.Count());

        }
        [Fact]
        public void GetNotesByKeyword_returnNotFound_keywordsEmptyOrNull()
        {
            // Given

            List<GeneralNotes> notes = NotesTestUtils.CreateMultipleGeneralNotes();
            List<string> keyWords = [];

            _mockService.Setup(service => service.GetGeneralNotesByKeyWords(keyWords)).Returns(notes);
            // When
            var result = _controller.GetNotesByKeyWord(keyWords);
            // Then
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);

        }
        [Fact]
        public void GetNotesByKeyword_returnNotFound_keywordsNotFound()
        {
            // Given
            List<GeneralNotes> notes = NotesTestUtils.CreateMultipleGeneralNotes();
            List<string> keyWords = ["Testing", "Testing", "Notes", "DashBoard"];

            _mockService.Setup(service => service.GetGeneralNotesByKeyWords(keyWords)).Returns(notes = []);
            // When
            var result = _controller.GetNotesByKeyWord(keyWords);
            // Then
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);

        }
        [Fact]
        public void GetNotesByOwner_returnsOk_andObject()
        {
            List<GeneralNotes> notes = NotesTestUtils.CreateMultipleGeneralNotes();
            string owner = "Owner Tommi_#1";
            IEnumerable<GeneralNotes> theNotes = [notes[2], notes[3]];
            _mockService.Setup(service => service.GetGeneralNotesByOwner(owner)).Returns(theNotes);

            // When
            var result = _controller.GetGeneralNotesByOwner(owner);

            // Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedNotes = Assert.IsAssignableFrom<IEnumerable<GeneralNotes>>(okResult.Value);
            Assert.Equal(2, returnedNotes.Count());

        }
        [Fact]
        public void GetNotesByOwner_returnsNotFond_ownerEmptyOrNull()
        {
            List<GeneralNotes> notes = NotesTestUtils.CreateMultipleGeneralNotes();
            string owner = "";
            IEnumerable<GeneralNotes> theNotes = [notes[2], notes[3]];
            _mockService.Setup(service => service.GetGeneralNotesByOwner(owner)).Returns(theNotes);

            // When
            var result = _controller.GetGeneralNotesByOwner(owner);

            // Then
            var notFoundResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        }
        [Fact]
        public void GetNotesByOwner_returnsNotFond_NotesNotFound()
        {
            List<GeneralNotes> notes = NotesTestUtils.CreateMultipleGeneralNotes();
            string owner = "TestOwner";
            IEnumerable<GeneralNotes> theNotes = [notes[2], notes[3]];
            _mockService.Setup(service => service.GetGeneralNotesByOwner(owner)).Returns(theNotes);

            // When
            var result = _controller.GetGeneralNotesByOwner("Jack");

            // Then
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public void GetNotesByDateCreated_returnsOk_andObject()
        {
            List<GeneralNotes> notes = NotesTestUtils.CreateMultipleGeneralNotes();

            IEnumerable<GeneralNotes> theNotes = [notes[2], notes[3]];
            DateTime theDate = DateTime.Now;
            _mockService.Setup(service => service.GetGeneralNotesByDateCreated(theDate)).Returns(theNotes);

            // When
            var result = _controller.GetNotestByDateCreated(theDate);

            // Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedNotes = Assert.IsAssignableFrom<IEnumerable<GeneralNotes>>(okResult.Value);
            Assert.Equal(2, returnedNotes.Count());
        }


        [Fact]
        public void UpdateGeneralNotes_returnOK_andObject()
        {
            // Given
            GeneralNotes notes = NotesTestUtils.CreateGeneralNotes();
            _mockService.Setup(service => service.UpdateGeneralNotes(notes.id, notes)).Returns(notes);

            // When
            var result = _controller.UpdateGeneralNotes(notes.id, notes);

            // Then
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnedNotes = Assert.IsAssignableFrom<GeneralNotes>(okResult.Value);
            Assert.NotNull(returnedNotes);
            Assert.Equal(notes.id, returnedNotes.id);
            Assert.Equal(notes.NotesTiltle, returnedNotes.NotesTiltle);
        }
        [Fact]
        public void UpdateGeneralNotes_returnNotFound_NotesNull()
        {
             // Given
            GeneralNotes notes = NotesTestUtils.CreateGeneralNotes();
            _mockService.Setup(service => service.UpdateGeneralNotes(notes.id, notes)).Returns(notes);

            // When
            var result = _controller.UpdateGeneralNotes(notes.id, null);

            // Then
            var notFoundResult = Assert.IsType<NotFoundResult>(result.Result);
        }
        [Fact]
        public void DeleteGenaralNotes_returnNotFound()
        {
            // Given
            Guid id = Guid.NewGuid();
            _mockService.Setup(service => service.DeleteGeneralNote(id)).Returns(false);

            // When
            var result = _controller.DeleteGenaralNotes(id);

            // Then
            Assert.IsType<NotFoundResult>(result);

        }
        [Fact]
        public void DeleteGenaralNotes_resturnNoContent()
        {
            // Given
            Guid id = Guid.NewGuid();
            _mockService.Setup(service => service.DeleteGeneralNote(id)).Returns(true);

            // When
            var result = _controller.DeleteGenaralNotes(id);

            // Then
            Assert.IsType<NoContentResult>(result);


        }
    }
}
