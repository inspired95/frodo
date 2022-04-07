import React from "react";

class TripProposalCard extends React.Component {
    constructor(props) {
        super(props);
        this.state = {

        }
      }

      render(){
          return(
            <div>
              <div class="card">
                    <img src="..." class="card-img-top" alt="..."/>
                    <div class="card-body">
                        <h5 class="card-title">Card title</h5>
                        <p class="card-text">This is a wider card with supporting text below as a natural lead-in to additional content. This content is a little bit longer.</p>
                        <p class="card-text"><small class="text-muted">Last updated 3 mins ago</small></p>
                    </div>
                </div>
            </div>
          )
      }
}


export default TripProposalCard;