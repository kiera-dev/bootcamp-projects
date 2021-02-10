const controller = require("../controllers/controller");
const mongoose = require('mongoose');

const QuoteSchema = new mongoose.Schema({
    text: {
        type: String,
        required: [true, "Duh you need to put a quote in"],
        minlength: [3, "Quote must be at least 3 characters"]
    },
    vote: {
        type: Number,
        default: 0,
    }
})

const AuthorSchema = new mongoose.Schema({
    name: {
        type: String,
        required: [true, "A name must be entered."],
        minlength: [3, "Name must be at least 3 characters in length."]
    },
    quotes: [QuoteSchema]
}, { timestamps: true });


const Quote = mongoose.model("quote", QuoteSchema);
const Author = mongoose.model("author", AuthorSchema);


module.exports = { Author, Quote }