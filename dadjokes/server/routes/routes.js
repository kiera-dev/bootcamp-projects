const controller = require("../controllers/controller");

module.exports = app => {
    app.get("/", controller.baseRoute);
    app.get("/api/jokes", controller.getAll);
    app.get("/api/jokes/:_id", controller.getOne);
    app.get("/api/random", controller.random);
    app.post("/api/jokes/new", controller.createJoke);
    app.put("/api/jokes/update/:_id", controller.updateJoke);
    app.delete("/api/jokes/delete/:_id", controller.deleteJoke);
}