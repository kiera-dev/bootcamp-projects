const { Author } = require('../models/models');

module.exports = {
    createAuthor: (req, res) => {
        Author.create(req.body)
            .then(data => res.json({ message: "success", data: data }))
            .catch(err => res.json({ message: "error", data: err }));
    },

    allAuthors: (req,res) => {
        Author.find()
            .then(data => res.json({ message: "success", data: data }))
            .catch(err => res.json({ message: "error", data: err }));
    },


    oneAuthor: (req,res) => {
        Author.findOne({ _id: req.params.id })
            .then(data => res.json({ message: "success", data: data }))
            .catch(err => res.json({ message: "error", data: err }));
    },
 

    updateAuthor: (req,res) => {
        Author.findOneAndUpdate({ _id: req.params.id }, req.body, { new: true, runValidators: true })
            .then(data => res.json({ message: "success", data: data }))
            .catch(err => res.json({ message: "error", data: err}))
    },


    deleteAuthor: (req,res) => {
        Author.deleteOne({ _id: req.params.id })
            .then(data => res.json({ message: "success", data: data }))
            .catch(err => res.json({ message: "error", data: err }));
    }
}
