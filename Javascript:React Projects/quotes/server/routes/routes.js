const controller = require('../controllers/controller');

module.exports = app => {
    app.get('/api/authors', controller.allAuthors);
    app.post('/api/authors', controller.createAuthor);
    app.get('/api/authors/:id', controller.oneAuthor);
    app.patch('/api/authors/:id', controller.updateAuthor);
    // app.delete('/api/authors/:id', controller.deleteAuthor);
    app.patch('/api/write/:id', controller.newQuote);
    app.patch('/api/delete/:qId', controller.deleteQuote);
    app.patch('/api/upvote/:id', controller.upvote);
    app.patch('/api/downvote/:id', controller.downvote);
}

//ok all the server side junk works in postman. now for the hard part T_T