const controller = require("../controllers/controller");
const mongoose = require('mongoose');


const ProdSchema = new mongoose.Schema({
    name: {
        type: String,
        required: [true, "A name must be entered."],
        minlength: [2, "Name must be at least 2 characters in length."]
    },
    price: { 
        type: Number,
        required: [true, "Price is required."],
        min: [0.01, "Must be at least $0.01"]
    },
    desc: { 
        type: String,
        required: [true, "description is required."],
        minlength: [5, "Must be at least 5 characters"]
    }
}, { timestamps: true });



const Product = mongoose.model("Product", ProdSchema);

module.exports = { Product }
