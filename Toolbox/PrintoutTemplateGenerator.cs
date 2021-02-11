using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bakery.Errors;
using Bakery.Models;
using DinkToPdf;

namespace Bakery.Toolbox
{
    public class PrintoutTemplateGenerator
    {
        public string GenerateHtml(Order order, List<OrdersAdditionals> ordersAdditionals, List<OrdersDecorations> ordersDecorations)
        {
            try
            {
                decimal additionalPrice = 0;
                additionalPrice += order.Cake.Price;
                additionalPrice += order.Cream.Price;

                if (ordersAdditionals.Count > 0)
                {
                    foreach (var additional in ordersAdditionals)
                    {
                        additionalPrice += additional.Additional.Price;
                    }
                }
                if (ordersDecorations.Count > 0)
                {
                    foreach(var decoration in ordersDecorations)
                    {
                        additionalPrice += decoration.Decoration.Price;
                    }
                }
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.AppendLine(@"
                    <!DOCTYPE html>
                    <html lang='p'>
                      <head>
                        <meta charset = 'utf-8'/>
                        <title> Zamówienie </title>
                        <meta name='viewport' content='width=device-width, initial-scale=1'/>
                        <link rel='icon' type='image/x-icon' href='favicon.ico'/>
                        <link href='https://fonts.googleapis.com/css?family=Roboto:300,400,500&display=swap' rel='stylesheet'>
                        <link href='https://fonts.googleapis.com/icon?family=Material+Icons' rel='stylesheet'>
                        <link rel='stylesheet' href='https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/css/bootstrap.min.css' integrity='sha384-ggOyR0iXCbMQv3Xipma34MD+dH/1fQ784/j6cY/iJTQUOhcWr7x9JvoRxT2MZw1T' crossorigin='anonymous'>
                        <script src='https://code.jquery.com/jquery-3.3.1.slim.min.js' integrity='sha384-q8i/X+965DzO0rT7abK41JStQIAqVgRVzpbzo5smXKp4YfRvH+8abtTE1Pi6jizo' crossorigin='anonymous' ></script>
                        <script src='https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.14.7/umd/popper.min.js' integrity='sha384-UO2eT0CpHqdSJQ6hJty5KVphtPhzWj9WO1clHTMGa3JDZwrnQq4sF86dIHNDz0W1' crossorigin='anonymous'></script>
                        <script src='https://stackpath.bootstrapcdn.com/bootstrap/4.3.1/js/bootstrap.min.js' integrity='sha384-JjSmVgyd0p3pXB1rRibZUAYoIIy6OrQ6VrjIEaFf/nJGzIxFDsf4x0xIM+B07jRM' crossorigin='anonymous'></script>
                      </head>
                ");
                stringBuilder.AppendFormat(@"
                    <body class='bg-light'>
                        <div class='container-fluid row col-12 d-flex justify-content-center'>
                            <div class='container-fluid row col-10 d-flex justify-content-between bg-dark text-white'>
                                <h3>Zamówienie - {0}</h3>
                                <h4>{1}</h4>
                            </div>", order.BaseProduct.Name, order.OrderedOnString);
                stringBuilder.AppendFormat(@"
                        <div class='container-fluid row col-10 d-flex justify-content-center bg-light text-dark mt-3'>
                            <h5>Biszkopt</h5>
                            <table style='width:100%'>
                              <tr>
                                <th>Nazwa</th>
                                <th>Opis</th>
                                <th>Cena</th>
                              </tr>
                              <tr>
                                <td>{0}</td>
                                <td>{1}</td>
                                <td>{2} zł/kg</td>
                              </tr>
                            </table>
                          </div>", order.Cake.Name, order.Cake.Description, order.Cake.Price);
                stringBuilder.AppendFormat(@"
                        <div class='container-fluid row col-10 d-flex justify-content-center bg-light text-dark mt-3'>
                             <h5>Krem</h5>
                                <table style='width: 100%'>
                                <tr>
                                  <th>Nazwa</th>
                                  <th>Opis</th>
                                  <th>Cena</th>
                                </tr>
                                <tr>
                                    <td>{0}</td>
                                    <td>{1}</td>
                                    <td>{2} zł/kg</td>
                                </tr>
                            </table>
                          </div>", order.Cream.Name, order.Cream.Description, order.Cream.Price);
                if (ordersAdditionals.Count > 0)
                {
                    stringBuilder.AppendLine(@"
                              <div class='container-fluid row col-10 d-flex justify-content-center bg-light text-dark mt-3'>
                                  <h5>Dodatki do tortu</h5>
                                  <table style='width: 100%'>
                                    <tr>
                                      <th>Nazwa</th>
                                      <th>Opis</th>
                                      <th>Ilość</th>
                                      <th>Cena</th>
                                    </tr>");
                    foreach (var additional in ordersAdditionals)
                    {
                        stringBuilder.AppendFormat(@"
                                    <tr>
                                      <th>{0}</th>
                                      <th>{1}</th>
                                      <th>{2}</th>
                                      <th>{3}</th>
                                    </tr>", additional.Additional.Name, additional.Additional.Description, additional.Quantity, additional.Additional.Price);
                    }
                    stringBuilder.AppendLine("</table></div>");
                }
                if (ordersDecorations.Count > 0)
                {
                    stringBuilder.AppendLine(@"
                              <div class='container-fluid row col-10 d-flex justify-content-center bg-light text-dark mt-3'>
                                  <h5>Dekoracje</h5>
                                  <table style='width: 100%'>
                                    <tr>
                                      <th>Nazwa</th>
                                      <th>Opis</th>
                                      <th>Ilość</th>
                                      <th>Cena</th>
                                    </tr>");
                    foreach(var decoration in ordersDecorations)
                    {
                        stringBuilder.AppendFormat(@"
                                    <tr>
                                      <th>{0}</th>
                                      <th>{1}</th>
                                      <th>{2}</th>
                                      <th>{3}</th>
                                    </tr>", decoration.Decoration.Name, decoration.Decoration.Description, decoration.Quantity, decoration.Decoration.Price);
                    }
                    stringBuilder.AppendLine("</table></div>");
                }

                stringBuilder.AppendFormat(@"
                                <div class='container-fluid row col-10 d-flex justify-content-between bg-light text-dark mt-3'>
                                    <h5>Cena bazowa: {0} zł/kg</h5>
                                    <h5>Dopłata: {1} zł/kg</h5>
                                  </div>
                                  <div class='container-fluid row col-10 d-flex justify-content-between bg-light text-dark'>
                                    <h5>Ilość kilogramów: {2}</h5>
                                    <h5>Cena końcowa: {3} zł/kg</h5>
                                  </div>
                                </div>
                               </body>
                             </html>", order.BaseProduct.Price, additionalPrice, order.Servings, order.TotalPrice);

                return stringBuilder.ToString();
            }
            catch(Exception ex)
            {
                LoggerError.Logger.Error(ex);
                return "";
            }
        }
    }
}
