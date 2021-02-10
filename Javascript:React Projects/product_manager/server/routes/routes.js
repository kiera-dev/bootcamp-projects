const controller = require('../controllers/controller');

module.exports = app => {
    app.get('/api/products', controller.allProducts);
    app.post('/api/products', controller.createProduct);
    app.get('/api/products/:id', controller.oneProduct);
    app.patch('/api/products/:id', controller.updateProduct);
    app.delete('/api/products/:id', controller.deleteProduct);
}
