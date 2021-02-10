const { Author } = require('../models/models');
// const { Quote } = require('../models/models');

module.exports = {
    createAuthor: (request, response) => {
        Author.create(request.body)
            .then(data => response.json({ message: "success", data: data }))
            .catch(err => response.json({ message: "error", data: err }));
    },

    allAuthors: (request,response) => {
        Author.find()
            .then(data => response.json({ message: "success", data: data }))
            .catch(err => response.json({ message: "error", data: err }));
    },


    oneAuthor: (request,response) => {
        Author.findOne({ _id: request.params.id })
            .then(data => response.json({ message: "success", data: data }))
            .catch(err => response.json({ message: "error", data: err }));
    },
 

    upvote: (request,response) => {
        Author.update({ 'quotes._id': request.params.id }, { $inc: { "quotes.$.vote": 1 }})
            .then(data => response.json({ message: "success", data: data }))
            .catch(err => response.json({ message: "error", data: err}));
    },

    downvote: (request,response) => {
        Author.update({ 'quotes._id': request.params.id }, { $inc: { "quotes.$.vote": -1 }})
            .then(data => response.json({ message: "success", data: data }))
            .catch(err => response.json({ message: "error", data: err}));
    },

    updateAuthor: (request,response) => {
        Author.findOneAndUpdate({ _id: request.params.id }, request.body, { new: true, runValidators: true })
            .then(data => response.json({ message: "success", data: data }))
            .catch(err => response.json({ message: "error", data: err}));
    },

    deleteAuthor: (request,response) => {
        Author.deleteOne({ _id: request.params.id })
            .then(data => response.json({ message: "success", data: data }))
            .catch(err => response.json({ message: "error", data: err }));
    },

    
    newQuote: (request,response) => {
        Author.updateOne({ _id: request.params.id }, { $addToSet: { quotes: request.body }}, { runValidators: true })
            .then(data => response.json(data))
            .catch(err => response.json({ message: "error", data: err }));
    },


    deleteQuote: (request,response) => {
        Author.updateOne({ 'quotes._id': request.params.qId }, { $pull: { quotes: { _id: request.params.qId }}})
            .then(data => response.json(data))
            .catch(err => response.json(err));
    }
}
//https://www.tutorialspoint.com/mongodb-increment-value-inside-nested-array