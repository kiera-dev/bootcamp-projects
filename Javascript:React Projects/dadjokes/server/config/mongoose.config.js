const mongoose = require("mongoose");

mongoose.connect("mongodb://localhost/jokes", {
    useNewUrlParser: true,
    useUnifiedTopology: true,    
    })
    .then(() => console.log("db connection established"))
    .catch(err => console.log("it's borked", err))
