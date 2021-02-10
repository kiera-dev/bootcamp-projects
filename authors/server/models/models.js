const controller = require("../controllers/controller");
const mongoose = require('mongoose');


const AuthorSchema = new mongoose.Schema({
    name: {
        type: String,
        required: [true, "A name must be entered."],
        minlength: [3, "Name must be at least 3 characters in length."]
    }
}, { timestamps: true });



const Author = mongoose.model("author", AuthorSchema);

module.exports = { Author }