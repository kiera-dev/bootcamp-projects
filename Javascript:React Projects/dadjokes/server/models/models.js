
const controller = require("../controllers/controller");
const mongoose = require("mongoose");

const JokeSchema = new mongoose.Schema(
    {
        setup: {
            type: String,
            required: [true, "setup required"],
            maxlength:[200, "boring"],
            minlength:[10, "too short"],
        },
        punchline: {
            type:String,
            required: [true, "punchline required"],
            minlength:[3, "too short"],
        },
    },{timestamps: true, collection: "joke"})

const Joke = mongoose.model("joke", JokeSchema);

module.exports = {
    Joke
}
