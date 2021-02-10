const express = require("express");
const app = express();
require('./config/mongoose.config');
const cors = require("cors");

app.use(cors());
app.use(express.json());
app.use(express.urlencoded({ extended: true }));

require("./routes/routes")(app);


app.listen(8000, () => console.log("Now listening on 8000 beep boop"));
