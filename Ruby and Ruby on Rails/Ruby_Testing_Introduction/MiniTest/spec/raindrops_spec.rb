require 'raindrops'

describe '#convert' do
  it 'returns "1" sound for 1' do
    expect(Raindrops.convert(1)).to eq '1'
  end

  it 'returns "Pling" sound for 3' do
    expect(Raindrops.convert(3)).to eq 'Pling'
  end

  it 'returns "Plang" sound for 5' do
    expect(Raindrops.convert(5)).to eq 'Plang'
  end

  it 'returns "Plong" sound for 7' do
    expect(Raindrops.convert(7)).to eq 'Plong'
  end

  it 'returns "Pling" sound for 6' do
    expect(Raindrops.convert(6)).to eq 'Pling'
  end

  it 'returns "Plang" sound for 10' do
    expect(Raindrops.convert(10)).to eq 'Plang'
  end

  it 'returns "Plong" sound for 14' do
    expect(Raindrops.convert(14)).to eq 'Plong'
  end

  it 'returns "8" sound for 8' do
    expect(Raindrops.convert(8)).to eq '8'
  end

  it 'returns "PlingPlangPlong" sound for 105' do
    expect(Raindrops.convert(105)).to eq 'PlingPlangPlong'
  end
end
