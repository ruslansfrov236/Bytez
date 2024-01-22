       
var fetchApi =  async  ()=> {
    try {
   
        var apiUrl = Stock/Index;

       
        var response = await fetch(apiUrl);

       
        if (!response.ok) {
            throw new Error(`HTTP error! Status: ${response.status}`);
        }

        var responseData = await response.text();

        console.log(responseData);
    } catch (error) {
        console.error('Error fetching data:', error);
    }
}

fetchApi();

var filterProduct = () => {

}
