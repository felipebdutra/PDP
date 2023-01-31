﻿using MediatR;
using MongoDB.Bson;
using PortfolioAnalyzer.Application.Interfaces;
using PortfolioAnalyzer.Core.BankAggregate;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PortfolioAnalyzer.Application.Commands.DeleteBank
{
    public class DeleteBankCommand : ICommand, IRequest
    {
        [Required]
        public string Name { get; set; }
    }
}
