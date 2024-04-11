const selector = new CategoriesSelector();

const filmCategories = [];
$("#filterFilmCategories option").each(function () {
    filmCategories.push({ id: parseInt($(this).val()) });
});

selector.render(filmCategories);