import pandas as pd
import plotly.graph_objs as go
from plotly.offline import plot


def get_price_data_by_date_scatter(price_data_by_date, scatter_name):
    price_plot = go.Scatter(x=price_data_by_date['Date'], y=price_data_by_date['Price'], name=scatter_name)

    return price_plot


def plot_outlier_and_no_outlier_data(outlier_data, no_outlier_data, output_file_name):
    outlier_scatter = get_price_data_by_date_scatter(outlier_data, "With Outliers")
    no_outlier_scatter = get_price_data_by_date_scatter(no_outlier_data, "No Outliers")
    layout = go.Layout(
        xaxis=dict(
            title='Date',
            titlefont=dict(
                family='Courier New, monospace',
                size=18,
                color='#7f7f7f'
            )
        ),
        yaxis=dict(
            title='Price',
            titlefont=dict(
                family='Courier New, monospace',
                size=18,
                color='#7f7f7f'
            )
        )
    )
    fig = go.Figure(data=[outlier_scatter, no_outlier_scatter], layout=layout)
    plot(fig, filename=output_file_name)


def main():
    file_with_outliers = r"C:\Users\meeha\source\repos\Outliers\OutlierData\Outliers.csv"
    file_without_outliers = r"C:\Users\meeha\source\repos\Outliers\OutlierData\WithoutOutliers.csv"
    output_file = r"C:\Users\meeha\source\repos\Outliers\OutlierData\outliers_vs_no_outliers_1.html"

    outlier_data = pd.read_csv(file_with_outliers)
    no_outlier_data = pd.read_csv(file_without_outliers)

    plot_outlier_and_no_outlier_data(outlier_data, no_outlier_data, output_file)


if __name__ == "__main__":
    main()
