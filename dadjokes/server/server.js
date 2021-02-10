const express = require("express");
const app = express();

require('./config/mongoose.config');

app.use(express.json());
app.use(express.urlencoded({ extended: true }));

require("./routes/routes")(app);
// require("./models/models")(app); //no

app.listen(8000, () => console.log("Now listening on 8000 beep boop"));
