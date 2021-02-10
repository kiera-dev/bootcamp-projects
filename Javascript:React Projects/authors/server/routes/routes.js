const controller = require('../controllers/controller');

module.exports = app => {
    app.get('/api/authors', controller.allAuthors);
    app.post('/api/authors', controller.createAuthor);
    app.get('/api/authors/:id', controller.oneAuthor);
    app.patch('/api/authors/:id', controller.updateAuthor);
    app.delete('/api/authors/:id', controller.deleteAuthor);
}