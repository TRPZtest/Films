
class CategoriesSelector {
    constructor(filmId) {
        this.cotainer = $('#categoriesSelector');
        this.baseURL = '/CategoriesApi/';
        this.filmId = filmId;
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
        if (!filmId)
            return [];
        return await this.fetchData(this.baseURL + 'CategoriesByFilmId', { filmId });
    }

    async getOptions() {
        const allCategories = await this.fetchAllCategories();
       
        const filmCategories = await this.fetchCategoriesByFilmId(this.filmId);

        const dictionary = Object.groupBy(allCategories, ({ parentCategoryId }) => parentCategoryId);
        const options = [];

        Object.keys(dictionary).forEach(key => {
            const value = dictionary[key];

            if (key === 'null') {
                value.forEach(x => options.push(({ id: x.id, text: x.name, selected: filmCategories.some(y => y.id === x.id) })));               
            }
            else {
                const parentCategoryName = allCategories.find(x => x.id === parseInt(key)).name;

                const children = value.map(({ id, name }) => ({ id, text: name, }));
                options.push({
                    text: parentCategoryName + ':',
                    children,
                });
            }
        });

        return options;
    }

    async render() {    
        const options = await this.getOptions();

       
        $(document).ready(() => {
            $(this.cotainer).select2({
                data: options
            });
        });
        console.log(options);
    }
}

const filmId = $('#Id').val();
let selector = new CategoriesSelector(filmId);

selector.render();

