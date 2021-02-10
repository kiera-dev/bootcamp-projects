import React from "react";

const Droids = props => {
    const {submitHandler, searchTerms, changeHandler} = props;
    return(
        <div>
            <p>These are not the droids youre looking for</p>
            <img src="https://static.wikia.nocookie.net/lotr/images/e/e7/Gandalf_the_Grey.jpg/revision/latest/top-crop/width/360/height/450?cb=20121110131754"></img>
        </div>
    )
}

export default Droids;