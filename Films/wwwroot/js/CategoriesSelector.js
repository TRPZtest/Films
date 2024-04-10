class СategoriesSelector {
    constructor() {
        this.container = document.querySelector('.categoriesSelector'); 
        this.relatedCategoriesContainer = {};
        this.rootCategoriesContainer = {};
        this.filmCategoriesContainer = {};
        this.inputsContainer = {};
        this.filmId = 4;  
        this.allCategories = [];
        this.baseURL = '/CategoriesApi/';           
   
        this.categoryOptionsContainer = {};
    }

    async fetchData(url, params = {}) {
        try {
            const queryParams = new URLSearchParams(params).toString();
            const response = await fetch(`${url}?${queryParams}`);
            if (!response.ok) {
                throw new Error('Failed to fetch data');
            }
            const data = await response.json();
            return data;
        } catch (error) {
            console.error('Error fetching data:', error);
            return [];
        }
    }

    async fetchAllCategories() {
        return await this.fetchData(this.baseURL + 'Categories');
    }

    async fetchCategoriesByFilmId(filmId) {
        return await this.fetchData(this.baseURL + 'CategoriesByFilmId', { filmId });
    }

    createOptionButton(category) {
        let btn = document.createElement("button");
        btn.className = 'btn btn-primary';
        btn.textContent = category.name;
        btn.setAttribute('categoryId', category.id);
        return btn;
    }

    async render() {
        this.setContainers();

        this.allCategories = await this.fetchAllCategories();

        let filmCategories = await this.fetchCategoriesByFilmId(this.filmId);

        let nonUsedCategories = this.allCategories.filter(x => !filmCategories.some(y => y.id === x.id)); //      

        this.renderFilmCategories(filmCategories);

        filmCategories.forEach(x => {
    
            let relatedCategories = nonUsedCategories.filter(y => y.parentCategoryId === x.id);

            this.renderRelatedCategories(relatedCategories, x.id);
        });

        let rootCategories = nonUsedCategories.filter(category => category.parentCategoryId === null);
        this.renderRelatedCategories(rootCategories);

        console.log(rootCategories);
    }

    renderFilmCategories(filmCategories) {
        var fragment = document.createDocumentFragment();
       
        filmCategories.forEach(category => fragment.appendChild(this.createOptionButton(category)));

        this.filmCategoriesContainer.appendChild(fragment);
    }

    renderRelatedCategories(categories, parentId) {            
        let fragment = document.createDocumentFragment();
        categories.forEach(category => fragment.appendChild(this.createOptionButton(category)));

        let container = document.createElement('div');
        container.id = `childs-${parentId}`;

        container.appendChild(fragment);
        this.relatedCategoriesContainer.appendChild(container);
    }

    renderRootCategories(categories) {
        let fragment = document.createDocumentFragment();
        categories.forEach(category => fragment.appendChild(this.createOptionButton(category)));
        this.rootCategoriesContainer.appendChild(fragment);
    }
   
    removeCategory() {

    }

    addCategory() {

    }

    setContainers() {     
        this.relatedCategoriesContainer = document.createElement('div');
        this.relatedCategoriesContainer.id = 'relatedCategoriesContainerId';
        this.container.appendChild(this.relatedCategoriesContainer);

        this.rootCategoriesContainer = document.createElement('div');
        this.rootCategoriesContainer.id = 'rootCategoriesContainerId';
        this.container.appendChild(this.rootCategoriesContainer);

        this.filmCategoriesContainer = document.createElement('div');
        this.filmCategoriesContainer.id = 'filmCategoriesContainerId';
        this.container.appendChild(this.filmCategoriesContainer);
    }
}

const categorySelector = new СategoriesSelector();
categorySelector.render();
