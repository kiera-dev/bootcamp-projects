const { Joke } = require('../models/models');


module.exports = {
    baseRoute: (request, response) => {
        response.json({ message:"it's working"})
    },

    getAll: (request, response) => {
        Joke.find()
        .then(data => response.json({message: "success", data: data}))
        .catch(err => response.json({message: "error", data: err}));
    },

    getOne: (request, response) => {
        Joke.findOne({_id: request.params._id})
        .then(data => response.json({message: "success", data: data}))
        .catch(err => response.json({message: "error", data: err}));
    },

    random: (request, response) => {
        Joke.aggregate([{$sample: {size: 1}}])
        .then(data => response.json({message: "success", data: data}))
        .catch(err => response.json({message: "error", data: err}));
    },

    createJoke: (request, response) => {
        Joke.create(request.body)
        .then(data => response.json({message: "success", data: data}))
        .catch(err => response.json({message: "error", data: err}));
    },

    updateJoke: (request, response) => {
        Joke.findOneAndUpdate({_id: request.params._id}, request.body)
        .then(data => response.json({message: "success", data: data}))
        .catch(err => response.json({message: "error", data: err}));
    },

    deleteJoke: (request, response) => {
        Joke.findOneAndDelete({_id: request.params._id})
        .then(data => response.json({message: "success", data: data}))
        .catch(err => response.json({message: "error", data: err}));
    },


}

//https://jsonformatter.org

//Ask Cody: 
//(node:25330) DeprecationWarning: Mongoose: `findOneAndUpdate()` and `findOneAndDelete()` without the `useFindAndModify` option set to false are deprecated. See: https://mongoosejs.com/docs/deprecations.html#findandmodify